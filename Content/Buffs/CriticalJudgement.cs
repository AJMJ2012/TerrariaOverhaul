using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrariaOverhaul.Content.Buffs;

public sealed class CriticalJudgement : ModBuff
{
	private static readonly SoundStyle StrikeSound = new SoundStyle($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Magic/MagicPowerfulBlast") {
		Pitch = 0.50f,
	};

	[Autoload(false)]
	private sealed class PlayerCriticalJudgementImplementation : ModPlayer
	{
		public bool Active;

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			TryApply(ref crit);
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			TryApply(ref crit);
		}

		public override void OnHitAnything(float x, float y, Entity victim)
		{
			if (Active) {
				Player.ClearBuff(ModContent.BuffType<CriticalJudgement>());
				Active = false;
			}
		}

		private void TryApply(ref bool crit)
		{
			if (!Active) {
				return;
			}

			int index = Player.FindBuffIndex(ModContent.BuffType<CriticalJudgement>());

			if (index >= 0) {
				crit = true;

				if (!Main.dedServ) {
					SoundEngine.PlaySound(StrikeSound);
				}

				Player.DelBuff(index);
			}

			Active = false;
		}
	}

	public override void Load()
	{
		Mod.AddContent<PlayerCriticalJudgementImplementation>();
	}

	public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
	{
		drawParams.DrawColor = Color.White; // Don't require hovering to draw at full opacity.

		return true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.TryGetModPlayer(out PlayerCriticalJudgementImplementation implementation)) {
			implementation.Active = true;

			// Glowing eye effect
			player.yoraiz0rEye = Math.Max(player.yoraiz0rEye, 2);
		}
	}
}
