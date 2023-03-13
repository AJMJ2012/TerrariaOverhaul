using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Guns;
using TerrariaOverhaul.Common.Tags;
// DA Creation
namespace TerrariaOverhaul.Common.ProjectileEffects;

[Autoload(Side = ModSide.Client)]
public class ProjectileLaserHitSound : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	public static readonly SoundStyle HitSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Projectiles/Laser/bullet_laser_0", 3) {
		Volume = 0.2f,
		PitchVariance = 0.2f
	};
	public bool appliesToEntity = false;

	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		LaserGun? info = null;
		(source as EntitySource_ItemUse)?.Item.TryGetGlobalItem<LaserGun>(out info);
		appliesToEntity = (info?.overhaulApplied ?? false) || OverhaulProjectileTags.LaserBullet.Has(projectile.type);
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
