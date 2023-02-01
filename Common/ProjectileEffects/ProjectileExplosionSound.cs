using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Tags;
// DA Creation
namespace TerrariaOverhaul.Common.ProjectileEffects;
public class ProjectileExplosionSound : GlobalProjectile
{
	public static readonly SoundStyle SmallExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/explosion_gunpowder_ver2_0", 6) {
		Volume = 0.6f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle Explosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/tnt_explode_0", 3) {
		Volume = 0.4f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle BigExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/explosion_general_medium_0", 3) {
		Volume = 0.4f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle GiantExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/explosion_medium_01") {
		Volume = 0.6f,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle SmallMagicExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/magic_explosion_small_0", 4) {
		Volume = 0.6f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle MagicExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/magic_explosion_0", 4) {
		Volume = 0.6f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle MagicExplosionTail = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/magic_explosion_tail_ver1_0", 4) {
		Volume = 0.8f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle MagicExplosionTail2 = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/magic_explosion_tail_ver3_0", 4) {
		Volume = 0.8f,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle ElectricExplosion = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Explosions/explosion_electric_0", 3) {
		Volume = 0.8f,
		PitchVariance = 0.2f
	};

	private Vector2 maxSize;

	public override bool InstancePerEntity => true;
	bool appliedExplosionSound = false;

	public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
		=> OverhaulProjectileTags.AnyExplosive(projectile.type);

	public override bool PreAI(Projectile projectile)
	{
		maxSize = Vector2.Max(maxSize, projectile.Size);

		return true;
	}
	public override void Kill(Projectile projectile, int timeLeft)
	{
		if (OverhaulProjectileTags.Explosive.Has(projectile.type) || OverhaulProjectileTags.MagicExplosive.Has(projectile.type) || OverhaulProjectileTags.ElectricExplosive.Has(projectile.type)) {
			bool types = projectile.type == ProjectileID.SpiritFlame || projectile.type == ProjectileID.DesertDjinnCurse;
			if ((types && timeLeft > 0) || !types) {
				PlayExplosionSound(projectile);
			}
		}
	}
	public override  bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
	{
		if (OverhaulProjectileTags.ExplosiveImpact.Has(projectile.type) || OverhaulProjectileTags.MagicExplosiveImpact.Has(projectile.type) || OverhaulProjectileTags.ElectricExplosiveImpact.Has(projectile.type)) {
			PlayExplosionSound(projectile);
		}
		return true;
	}
	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		if (OverhaulProjectileTags.Explosions.Has(projectile.type) || OverhaulProjectileTags.MagicExplosions.Has(projectile.type) || OverhaulProjectileTags.ElectricExplosions.Has(projectile.type)) {
			PlayExplosionSound(projectile);
		}
	}

	public void PlayExplosionSound(Projectile projectile)
	{
		if (appliedExplosionSound) return;
		appliedExplosionSound = true;
		maxSize = Vector2.Max(maxSize, projectile.Size);

		if (maxSize.X <= 0f || maxSize.Y <= 0f) {
			return;
		}

		float maxPower = (float)Math.Sqrt(maxSize.X * maxSize.Y);

		//TODO: Hardcoded cuz tired.
		if (projectile.type == ProjectileID.ExplosiveBullet) {
			maxPower = 10f;
		}

		// TODO: Prevent original sounds
		// TODO: Detect original sound calls and replace them instead
		if (OverhaulProjectileTags.ElectricExplosive.Has(projectile.type) || OverhaulProjectileTags.ElectricExplosiveImpact.Has(projectile.type) || OverhaulProjectileTags.ElectricExplosions.Has(projectile.type)) {
			SoundEngine.PlaySound(ElectricExplosion, projectile.Center);
		}
		else if (OverhaulProjectileTags.MagicExplosive.Has(projectile.type) || OverhaulProjectileTags.MagicExplosiveImpact.Has(projectile.type) || OverhaulProjectileTags.MagicExplosions.Has(projectile.type)) {
			if (maxPower < 50)
				SoundEngine.PlaySound(SmallMagicExplosion, projectile.Center);
			else {
				if (maxPower >= 50)
					SoundEngine.PlaySound(MagicExplosion, projectile.Center);
				if (maxPower >= 100)
					SoundEngine.PlaySound(MagicExplosionTail, projectile.Center);
				if (maxPower >= 150)
					SoundEngine.PlaySound(MagicExplosionTail2, projectile.Center);
			}
		}
		else {
			if (maxPower < 50)
				SoundEngine.PlaySound(SmallExplosion, projectile.Center);
			else {
				if (maxPower >= 50)
					SoundEngine.PlaySound(Explosion, projectile.Center);
				if (maxPower >= 150)
					SoundEngine.PlaySound(BigExplosion, projectile.Center);
				if (maxPower >= 250)
					SoundEngine.PlaySound(GiantExplosion, projectile.Center);
			}
		}
	}
}
