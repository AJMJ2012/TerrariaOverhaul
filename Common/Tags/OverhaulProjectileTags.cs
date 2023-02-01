using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Core.Tags;
using TerrariaOverhaul.Utilities;
using Group = TerrariaOverhaul.Core.Tags.ProjectileTags;
// DA Edit
namespace TerrariaOverhaul.Common.Tags;

public sealed class OverhaulProjectileTags : ILoadable
{
	/// <summary> These set things on fire. Enough said. </summary>
	public static readonly TagData Incendiary = ContentTags.Get<Group>(nameof(Incendiary));

	/// <summary> Projectiles with this tag extinguish fires and interact with incendiary projectiles. </summary>
	public static readonly TagData Extinguisher = ContentTags.Get<Group>(nameof(Extinguisher));

	/// <summary> Ice related, leaves ice decals. </summary>
	public static readonly TagData Ice = ContentTags.Get<Group>(nameof(Ice));

	/// <summary> Grappling hooks with this tag won't have Overhaul's physics improvements. </summary>
	public static readonly TagData NoGrapplingHookSwinging = ContentTags.Get<Group>(nameof(NoGrapplingHookSwinging));

	/// <summary> Adds screenshake and extra knockback when this projectile is killed. </summary>
	public static readonly TagData Explosive = ContentTags.Get<Group>(nameof(Explosive));

	/// <summary> Changes audio on tile collision. </summary>
	public static readonly TagData Bullet = ContentTags.Get<Group>(nameof(Bullet));

	/// <summary> Used in determining whether something's a rocket launcher or a grenade launcher. </summary>
	public static readonly TagData Rocket = ContentTags.Get<Group>(nameof(Rocket));

	/// <summary> Used in determining whether something's a rocket launcher or a grenade launcher. </summary>
	public static readonly TagData Grenade = ContentTags.Get<Group>(nameof(Grenade));

	/// <summary> Mostly used for extra visuals, like 'gore' pieces. </summary>
	public static readonly TagData WoodenArrow = ContentTags.Get<Group>(nameof(WoodenArrow));

	public static readonly TagData LaserBullet = ContentTags.Get<Group>(nameof(LaserBullet));
	public static readonly TagData ExplosiveImpact = ContentTags.Get<Group>(nameof(ExplosiveImpact));
	public static readonly TagData Explosions = ContentTags.Get<Group>(nameof(Explosions));
	public static readonly TagData MagicExplosive = ContentTags.Get<Group>(nameof(MagicExplosive));
	public static readonly TagData MagicExplosiveImpact = ContentTags.Get<Group>(nameof(MagicExplosiveImpact));
	public static readonly TagData MagicExplosions = ContentTags.Get<Group>(nameof(MagicExplosions));
	public static readonly TagData ElectricExplosive = ContentTags.Get<Group>(nameof(ElectricExplosive));
	public static readonly TagData ElectricExplosiveImpact = ContentTags.Get<Group>(nameof(ElectricExplosiveImpact));
	public static readonly TagData ElectricExplosions = ContentTags.Get<Group>(nameof(ElectricExplosions));
	public static bool AnyExplosive(int type) => Explosive.Has(type) || Explosions.Has(type) || MagicExplosive.Has(type) || MagicExplosiveImpact.Has(type) || MagicExplosions.Has(type) || ElectricExplosive.Has(type) || ElectricExplosiveImpact.Has(type) || ElectricExplosions.Has(type);
	void ILoadable.Load(Mod mod)
	{
		Incendiary.SetMultiple(
			ProjectileID.Spark,
			ProjectileID.FlamesTrap,
			ProjectileID.GreekFire1,
			ProjectileID.GreekFire2,
			ProjectileID.GreekFire3,
			ProjectileID.MolotovCocktail,
			ProjectileID.MolotovFire,
			ProjectileID.MolotovFire2,
			ProjectileID.MolotovFire3,
			ProjectileID.CultistBossFireBall,
			ProjectileID.DD2BetsyFireball,
			ProjectileID.DD2BetsyFlameBreath,
			ProjectileID.InfernoFriendlyBlast,
			ProjectileID.InfernoHostileBlast,
			ProjectileID.Fireball,
			ProjectileID.BallofFire,
			ProjectileID.Flamelash,
			ProjectileID.Flames,
			ProjectileID.FireArrow,
			ProjectileID.Meteor1,
			ProjectileID.Meteor2,
			ProjectileID.Meteor3,
			ProjectileID.Flare
		);

		Ice.SetMultiple(
			ProjectileID.CultistBossIceMist,
			ProjectileID.IceBolt,
			ProjectileID.IceBlock,
			ProjectileID.IceBoomerang,
			ProjectileID.IceSpike,
			ProjectileID.IcewaterSpit,
			ProjectileID.IceSickle,
			ProjectileID.BallofFrost,
			ProjectileID.FrostArrow,
			ProjectileID.FrostBeam,
			ProjectileID.FrostBlastFriendly,
			ProjectileID.FrostBlastHostile,
			ProjectileID.FrostBoltStaff,
			ProjectileID.FrostBoltSword,
			ProjectileID.FrostburnArrow,
			ProjectileID.FrostDaggerfish,
			ProjectileID.FrostHydra,
			ProjectileID.FrostShard,
			ProjectileID.FrostWave,
			ProjectileID.BabySnowman,
			ProjectileID.NorthPoleSnowflake,
			ProjectileID.SnowBallFriendly,
			ProjectileID.SnowBallHostile
		);

		Extinguisher.SetMultiple(
			ProjectileID.WaterStream,
			ProjectileID.WaterBolt,
			ProjectileID.HolyWater,
			ProjectileID.UnholyWater,
			ProjectileID.BloodWater,
			ProjectileID.WaterGun,
			ProjectileID.CultistBossIceMist,
			ProjectileID.IceBolt,
			ProjectileID.IceBlock,
			ProjectileID.IceBoomerang,
			ProjectileID.IceSpike,
			ProjectileID.IcewaterSpit,
			ProjectileID.IceSickle,
			ProjectileID.BallofFrost,
			ProjectileID.FrostArrow,
			ProjectileID.FrostBeam,
			ProjectileID.FrostBlastFriendly,
			ProjectileID.FrostBlastHostile,
			ProjectileID.FrostBoltStaff,
			ProjectileID.FrostBoltSword,
			ProjectileID.FrostburnArrow,
			ProjectileID.FrostDaggerfish,
			ProjectileID.FrostHydra,
			ProjectileID.FrostShard,
			ProjectileID.FrostWave,
			ProjectileID.GoldenShowerFriendly,
			ProjectileID.GoldenShowerHostile,
			ProjectileID.BabySnowman,
			ProjectileID.NorthPoleSnowflake,
			ProjectileID.SnowBallFriendly,
			ProjectileID.SnowBallHostile,
			ProjectileID.BlueFlare,
			ProjectileID.BloodRain,
			ProjectileID.BloodCloudMoving,
			ProjectileID.BloodCloudRaining,
			ProjectileID.BloodWater,
			ProjectileID.RainCloudMoving,
			ProjectileID.RainCloudRaining,
			ProjectileID.RainFriendly,
			ProjectileID.RainNimbus
		);

		NoGrapplingHookSwinging.SetMultiple(
			ProjectileID.QueenSlimeHook,
			ProjectileID.AntiGravityHook,
			ProjectileID.StaticHook
		);

		Explosive.SetMultiple(
			ProjectileID.BlackBolt,
			ProjectileID.Bomb,
			ProjectileID.BouncyBomb,
			ProjectileID.StickyBomb,
			ProjectileID.Grenade,
			ProjectileID.BouncyGrenade,
			ProjectileID.StickyGrenade,
			ProjectileID.RocketI,
			ProjectileID.RocketII,
			ProjectileID.RocketIII,
			ProjectileID.RocketIV,
			ProjectileID.GrenadeI,
			ProjectileID.GrenadeII,
			ProjectileID.GrenadeIII,
			ProjectileID.GrenadeIV,
			ProjectileID.NailFriendly,
			ProjectileID.HellfireArrow,
			ProjectileID.Dynamite,
			ProjectileID.BouncyDynamite,
			ProjectileID.StickyDynamite,
			ProjectileID.BombFish,
			ProjectileID.Meteor1,
			ProjectileID.Meteor2,
			ProjectileID.Meteor3,
			ProjectileID.ExplosiveBullet,
			// DA Additions
			ProjectileID.ApprenticeStaffT3Shot,
			ProjectileID.Beenade,
			ProjectileID.BombSkeletronPrime,
			ProjectileID.CannonballFriendly,
			ProjectileID.CannonballHostile,
			ProjectileID.Celeb2Rocket,
			ProjectileID.Celeb2RocketExplosive,
			ProjectileID.Celeb2RocketExplosiveLarge,
			ProjectileID.Celeb2RocketLarge,
			ProjectileID.ClusterFragmentsI,
			ProjectileID.ClusterFragmentsII,
			ProjectileID.ClusterGrenadeI,
			ProjectileID.ClusterGrenadeII,
			ProjectileID.ClusterMineI,
			ProjectileID.ClusterMineII,
			ProjectileID.ClusterRocketI,
			ProjectileID.ClusterRocketII,
			ProjectileID.ClusterSnowmanRocketI,
			ProjectileID.ClusterSnowmanRocketII,
			ProjectileID.CultistBossFireBallClone,
			ProjectileID.DD2BetsyArrow,
			ProjectileID.DryGrenade,
			ProjectileID.DryMine,
			ProjectileID.DryRocket,
			ProjectileID.DrySnowmanRocket,
			ProjectileID.ElectrosphereMissile,
			ProjectileID.Fireball,
			ProjectileID.HoneyGrenade,
			ProjectileID.HoneyMine,
			ProjectileID.HoneyRocket,
			ProjectileID.HoneySnowmanRocket,
			ProjectileID.JackOLantern,
			ProjectileID.LavaGrenade,
			ProjectileID.LavaMine,
			ProjectileID.LavaRocket,
			ProjectileID.LavaSnowmanRocket,
			ProjectileID.MiniNukeGrenadeI,
			ProjectileID.MiniNukeGrenadeII,
			ProjectileID.MiniNukeMineI,
			ProjectileID.MiniNukeMineII,
			ProjectileID.MiniNukeRocketI,
			ProjectileID.MiniNukeRocketII,
			ProjectileID.MiniNukeSnowmanRocketI,
			ProjectileID.MiniNukeSnowmanRocketII,
			ProjectileID.Nail,
			ProjectileID.RocketFireworkBlue,
			ProjectileID.RocketFireworkGreen,
			ProjectileID.RocketFireworkRed,
			ProjectileID.RocketFireworkYellow,
			ProjectileID.RocketSkeleton,
			ProjectileID.RocketSnowmanI,
			ProjectileID.RocketSnowmanII,
			ProjectileID.RocketSnowmanIII,
			ProjectileID.RocketSnowmanIV,
			ProjectileID.SantankMountRocket,
			ProjectileID.StarCannonStar,
			ProjectileID.Stynger,
			ProjectileID.StyngerShrapnel,
			ProjectileID.VortexBeaterRocket,
			ProjectileID.WetGrenade,
			ProjectileID.WetSnowmanRocket
		);

		Explosions.SetMultiple(
			ProjectileID.DD2ExplosiveTrapT1Explosion,
			ProjectileID.DD2ExplosiveTrapT2Explosion,
			ProjectileID.DD2ExplosiveTrapT3Explosion,
			ProjectileID.DaybreakExplosion,
			ProjectileID.SolarWhipSwordExplosion
		);

		// Magical explosions. Not physical or fire magic type explosions.
		MagicExplosive.SetMultiple(
			ProjectileID.DesertDjinnCurse,
			ProjectileID.FallingStar,
			ProjectileID.HallowStar,
			ProjectileID.NebulaArcanum,
			ProjectileID.NebulaBlaze1,
			ProjectileID.NebulaBlaze2,
			ProjectileID.SpiritFlame,
			ProjectileID.StarWrath,
			ProjectileID.SuperStar
		);

		MagicExplosiveImpact.SetMultiple(
			ProjectileID.LunarFlare
		);

		MagicExplosions.SetMultiple(
			ProjectileID.NebulaArcanumExplosionShot,
			ProjectileID.StardustGuardianExplosion,
			ProjectileID.RainbowCrystalExplosion
		);

		// Electrical explosions.
		ElectricExplosive.SetMultiple();

		ElectricExplosiveImpact.SetMultiple();

		ElectricExplosions.SetMultiple(
			ProjectileID.Electrosphere
		);

		Bullet.SetMultiple(
			ProjectileID.Bullet,
			ProjectileID.BulletHighVelocity,
			ProjectileID.BulletSnowman,
			ProjectileID.BulletDeadeye,
			ProjectileID.ChlorophyteBullet,
			ProjectileID.CrystalBullet,
			ProjectileID.CursedBullet,
			ProjectileID.ExplosiveBullet,
			ProjectileID.GoldenBullet,
			ProjectileID.IchorBullet,
			ProjectileID.MoonlordBullet,
			ProjectileID.NanoBullet,
			ProjectileID.PartyBullet,
			ProjectileID.SniperBullet,
			ProjectileID.VenomBullet,
			ProjectileID.MeteorShot
		);

		Rocket.SetMultiple(
			ProjectileID.RocketI,
			ProjectileID.RocketII,
			ProjectileID.RocketIII,
			ProjectileID.RocketIV,
			// DA Additions
			ProjectileID.ClusterRocketI,
			ProjectileID.ClusterRocketII,
			ProjectileID.ClusterSnowmanRocketI,
			ProjectileID.ClusterSnowmanRocketII,
			ProjectileID.DryRocket,
			ProjectileID.DrySnowmanRocket,
			ProjectileID.HoneyRocket,
			ProjectileID.HoneySnowmanRocket,
			ProjectileID.LavaRocket,
			ProjectileID.LavaSnowmanRocket,
			ProjectileID.MiniNukeRocketI,
			ProjectileID.MiniNukeRocketII,
			ProjectileID.MiniNukeSnowmanRocketI,
			ProjectileID.MiniNukeSnowmanRocketII,
			ProjectileID.RocketSkeleton,
			ProjectileID.RocketSnowmanI,
			ProjectileID.RocketSnowmanII,
			ProjectileID.RocketSnowmanIII,
			ProjectileID.RocketSnowmanIV,
			ProjectileID.SantankMountRocket,
			ProjectileID.Stynger,
			ProjectileID.VortexBeaterRocket,
			ProjectileID.WetRocket,
			ProjectileID.WetSnowmanRocket
		);

		Grenade.SetMultiple(
			ProjectileID.Grenade,
			ProjectileID.BouncyGrenade,
			ProjectileID.StickyGrenade,
			ProjectileID.GrenadeI,
			ProjectileID.GrenadeII,
			ProjectileID.GrenadeIII,
			ProjectileID.GrenadeIV,
			// DA Additions
			ProjectileID.ClusterFragmentsI,
			ProjectileID.ClusterFragmentsII,
			ProjectileID.ClusterGrenadeI,
			ProjectileID.ClusterGrenadeII,
			ProjectileID.ClusterMineI,
			ProjectileID.ClusterMineII,
			ProjectileID.DryGrenade,
			ProjectileID.DryMine,
			ProjectileID.HoneyGrenade,
			ProjectileID.HoneyMine,
			ProjectileID.JackOLantern,
			ProjectileID.LavaGrenade,
			ProjectileID.LavaMine,
			ProjectileID.MiniNukeGrenadeI,
			ProjectileID.MiniNukeGrenadeII,
			ProjectileID.MiniNukeMineI,
			ProjectileID.MiniNukeMineII,
			ProjectileID.StyngerShrapnel,
			ProjectileID.WetGrenade,
			ProjectileID.WetMine
		);

		WoodenArrow.SetMultiple(
			ProjectileID.WoodenArrowFriendly,
			ProjectileID.WoodenArrowHostile,
			ProjectileID.FireArrow
		);

		LaserBullet.SetMultiple(
			ProjectileID.DeathLaser,
			ProjectileID.EyeBeam,
			ProjectileID.EyeLaser,
			ProjectileID.FrostBeam,
			ProjectileID.GreenLaser,
			ProjectileID.HeatRay,
			ProjectileID.LaserMachinegunLaser,
			ProjectileID.MartianWalkerLaser,
			ProjectileID.MinecartMechLaser,
			ProjectileID.MiniRetinaLaser,
			ProjectileID.NebulaLaser,
			ProjectileID.PinkLaser,
			ProjectileID.PurpleLaser,
			ProjectileID.RayGunnerLaser,
			ProjectileID.SaucerLaser,
			ProjectileID.ScutlixLaser,
			ProjectileID.ScutlixLaserFriendly,
			ProjectileID.ShadowBeamFriendly,
			ProjectileID.ShadowBeamHostile,
			ProjectileID.UFOLaser,
			ProjectileID.VortexLaser,
			ProjectileID.ZapinatorLaser
		);
	}
	void ILoadable.Unload() { }
}
