﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Systems.Camera;
using TerrariaOverhaul.Core.Systems.Debugging;
using TerrariaOverhaul.Utilities;
using TerrariaOverhaul.Utilities.DataStructures;
using TerrariaOverhaul.Utilities.Extensions;

namespace TerrariaOverhaul.Common.Systems.AudioEffects
{
	//TODO: Add configuration.
	[Autoload(Side = ModSide.Client)]
	public class AudioEffectsSystem : ModSystem
	{
		private struct SoundInstanceData
		{
			public readonly WeakReference<SoundEffectInstance> Instance;
			public readonly WeakReference<ActiveSound> TrackedSound;
			public readonly Vector2? StartPosition;

			public bool firstUpdate;
			public float localLowPassFiltering;

			public SoundInstanceData(SoundEffectInstance instance, Vector2? initialPosition = null, ActiveSound trackedSound = null)
			{
				Instance = new WeakReference<SoundEffectInstance>(instance);
				TrackedSound = trackedSound != null ? new WeakReference<ActiveSound>(trackedSound) : null;
				StartPosition = initialPosition;

				firstUpdate = true;
				localLowPassFiltering = 0f;
			}
		}

		private static readonly List<AudioEffectsModifier> Modifiers = new();

		private static AudioEffectParameters soundParameters;
		private static AudioEffectParameters musicParameters;
		private static Action<SoundEffectInstance, float> applyReverbFunc;
		private static Action<SoundEffectInstance, float> applyLowPassFilteringFunc;
		private static FieldInfo soundEffectBasedAudioTrackInstanceField;
		private static List<SoundInstanceData> trackedSoundInstances;

		public static bool IsEnabled { get; private set; }
		public static bool ReverbEnabled { get; private set; }
		public static bool LowPassFilteringEnabled { get; private set; }

		public override void Load()
		{
			IsEnabled = false;

			applyReverbFunc = typeof(SoundEffectInstance)
				.GetMethod("INTERNAL_applyReverb", BindingFlags.Instance | BindingFlags.NonPublic)
				?.CreateDelegate<Action<SoundEffectInstance, float>>();

			applyLowPassFilteringFunc = typeof(SoundEffectInstance)
				.GetMethod("INTERNAL_applyLowPassFilter", BindingFlags.Instance | BindingFlags.NonPublic)
				?.CreateDelegate<Action<SoundEffectInstance, float>>();

			soundEffectBasedAudioTrackInstanceField = typeof(ASoundEffectBasedAudioTrack)
				.GetField("_soundEffectInstance", BindingFlags.Instance | BindingFlags.NonPublic);

			trackedSoundInstances = new List<SoundInstanceData>();
			IsEnabled = applyReverbFunc != null && applyLowPassFilteringFunc != null;

			On.Terraria.Audio.ActiveSound.Play += (orig, activeSound) => {
				orig(activeSound);

				if(activeSound.Sound?.IsDisposed == false) {
					trackedSoundInstances.Add(new SoundInstanceData(activeSound.Sound, activeSound.Position, activeSound));
				}
			};

			On.Terraria.Audio.LegacySoundPlayer.PlaySound += (orig, soundPlayer, type, x, y, style, volumeScale, pitchOffset) => {
				var result = orig(soundPlayer, type, x, y, style, volumeScale, pitchOffset);

				if(result != null && trackedSoundInstances != null) {
					Vector2? position = x >= 0 && y >= 0 ? new Vector2(x, y) : null;

					trackedSoundInstances.Add(new SoundInstanceData(result, position));
				}

				return result;
			};

			DebugSystem.Log(IsEnabled ? "Audio effects enabled." : "Audio effects disabled: Internal FNA methods are missing.");
		}

		public override void PostUpdateEverything()
		{
			//Update global values
			ReverbEnabled = true;
			LowPassFilteringEnabled = true;

			AudioEffectParameters newSoundParameters = default;
			AudioEffectParameters newMusicParameters = default;

			for(int i = 0; i < Modifiers.Count; i++) {
				var modifier = Modifiers[i];

				modifier.Modifier(modifier.TimeLeft / (float)modifier.TimeMax, ref newSoundParameters, ref newMusicParameters);

				if(--modifier.TimeLeft <= 0) {
					Modifiers.RemoveAt(i--);
				} else {
					Modifiers[i] = modifier;
				}
			}

			soundParameters = newSoundParameters;
			musicParameters = newMusicParameters;

			if(IsEnabled) {
				bool fullUpdate = Main.GameUpdateCount % 4 == 0;

				//Update sound instances
				for(int i = 0; i < trackedSoundInstances.Count; i++) {
					var data = trackedSoundInstances[i];

					if(!UpdateSoundData(ref data, fullUpdate)) {
						trackedSoundInstances.RemoveAt(i--);
						continue;
					}

					trackedSoundInstances[i] = data;
				}

				if(Main.audioSystem is LegacyAudioSystem legacyAudioSystem) {
					for(int i = 0; i < legacyAudioSystem.AudioTracks.Length; i++) {
						if(legacyAudioSystem.AudioTracks[i] is ASoundEffectBasedAudioTrack soundEffectTrack) {
							var instance = (DynamicSoundEffectInstance)soundEffectBasedAudioTrackInstanceField.GetValue(soundEffectTrack);

							if(instance?.IsDisposed == false) {
								ApplyEffects(instance, musicParameters);
							}
						}
					}
				}
			}
		}

		public static void AddAudioEffectModifier(int time, string identifier, AudioEffectsModifier.ModifierDelegate func)
		{
			int existingIndex = Modifiers.FindIndex(m => m.Id == identifier);

			if(existingIndex < 0) {
				Modifiers.Add(new AudioEffectsModifier(time, identifier, func));
				return;
			}

			var modifier = Modifiers[existingIndex];

			modifier.TimeLeft = Math.Max(modifier.TimeLeft, time);
			modifier.TimeMax = Math.Max(modifier.TimeMax, time);
			modifier.Modifier = func;

			Modifiers[existingIndex] = modifier;
		}

		private static void ApplyEffects(SoundEffectInstance instance, AudioEffectParameters parameters)
		{
			if(ReverbEnabled) {
				applyReverbFunc(instance, parameters.Reverb);
			}

			if(LowPassFilteringEnabled) {
				applyLowPassFilteringFunc(instance, 1f - (parameters.LowPassFiltering * 0.9f));
			}
		}
		private static bool UpdateSoundData(ref SoundInstanceData data, bool fullUpdate)
		{
			if(!data.Instance.TryGetTarget(out var instance) || instance.IsDisposed || instance.State != SoundState.Playing) {
				return false;
			}

			if(fullUpdate || data.firstUpdate) {
				UpdateSoundOcclusion(ref data);
			}

			var localParameters = soundParameters;

			localParameters.LowPassFiltering += data.localLowPassFiltering;

			ApplyEffects(instance, localParameters);

			data.firstUpdate = false;

			return true;
		}
		private static void UpdateSoundOcclusion(ref SoundInstanceData data)
		{
			Vector2? soundPosition;

			if(data.TrackedSound != null && data.TrackedSound.TryGetTarget(out var trackedSound)) {
				soundPosition = trackedSound.Position;
			} else {
				soundPosition = data.StartPosition;
			}

			if(!soundPosition.HasValue) {
				return;
			}

			int occludingTiles = 0;

			const int MaxOccludingTiles = 15;

			GeometryUtils.BresenhamLine(
				CameraSystem.ScreenCenter.ToTileCoordinates(),
				soundPosition.Value.ToTileCoordinates(),
				(Vector2Int point, ref bool stop) => {
					if(!Main.tile.TryGet(point, out var tile)) {
						stop = true;
						return;
					}

					if(tile.IsActive && Main.tileSolid[tile.type]) {
						occludingTiles++;

						if(occludingTiles >= MaxOccludingTiles) {
							stop = true;
						}
					}
				}
			);

			data.localLowPassFiltering = occludingTiles / (float)MaxOccludingTiles;
		}
	}
}