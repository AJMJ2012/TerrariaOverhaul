using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
// DA Creation
namespace TerrariaOverhaul.Common.ProjectileEffects;

[Autoload(Side = ModSide.Client)]
public class ProjectileMagicHitSound : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	public static readonly SoundStyle HitSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Projectiles/Magic/spell_ricochet_general", 3) {
		Volume = 0.2f,
		PitchVariance = 0.2f
	};
	public bool appliesToEntity = false;

	// Don't want to include laser hit sounds.
	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		projectile.TryGetGlobalProjectile<ProjectileLaserHitSound>(out var info);
		appliesToEntity = !(info?.appliesToEntity ?? false) && projectile.DamageType == DamageClass.Magic;
	}

	public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
	{
		if (appliesToEntity)
		{
			SoundEngine.PlaySound(HitSound, projectile.Center);
		}
		return true;
	}
}
