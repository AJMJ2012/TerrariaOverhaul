using Terraria;
using Terraria.ID;
using TerrariaOverhaul.Common.Tags;
using TerrariaOverhaul.Utilities;
// DA Creation
namespace TerrariaOverhaul.Common.Guns;

public static class GunChecks {

	const int InsanelyFast = 8;
	const int VeryFast = 20;
	const int Fast = 25;
	const int Average = 30;
	const int Slow = 35;
	const int VerySlow = 45;
	const int ExtremelySlow = 55;

	public static bool IsGun(Item item) {
		if (item.channel) {
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);
			if (projectile.aiStyle == ProjAIStyleID.HeldProjectile) {
				return false;
			}
			if (item.UseSound == null) {
				return false;
			}
			if (item.noUseGraphic) {
				return false;
			}
		}
		return item.useAmmo == AmmoID.Bullet && item.shoot > 0;
	}

	public static bool AssaultRifleCheck(Item item) {
		return IsGun(item) && !HandgunCheck(item) && !BoltRifleCheck(item) && !ShotgunCheck(item) && !MinigunCheck(item) && !LaserGunCheck(item);
	}

	public static bool HandgunCheck(Item item) {
		// Exclude auto reuse
		if (item.autoReuse) {
			return false;
		}
		return IsGun(item) && !BoltRifleCheck(item) && !ShotgunCheck(item) && !MinigunCheck(item) && !LaserGunCheck(item);
	}

	public static bool BoltRifleCheck(Item item) {
		// Exclude non average and faster weapons
		if (item.useTime < Average) {
			return false;
		}

		return IsGun(item) && !ShotgunCheck(item) && !LaserGunCheck(item);
	}

	// TODO: Multiple Projectile Check somehow...
	public static bool ShotgunCheck(Item item) {
		// Include Xenopopper
		if (item.type == ItemID.Xenopopper) {
			return true;
		}

		// Only Shotgun Sounds
		if (item.UseSound != SoundID.Item36 && item.UseSound != SoundID.Item38) {
			return false;
		}

		// Exclude insanely fast weapons
		if (item.useTime <= InsanelyFast) {
			return false;
		}

		// Not high velocity bullets
		if (item.shoot == ProjectileID.BulletHighVelocity) {
			return false;
		}

		return IsGun(item) && !LaserGunCheck(item);
	}

	public static bool MinigunCheck(Item item) {
		// Include vanilla miniguns
		if (item.type == ItemID.Minishark || item.type == ItemID.Gatligator || item.type == ItemID.ChainGun) {
			return true;
		}

		// Exclude non gatling, minigun, or chaingun named weapons
		if (!item.Name.Contains("Gatling") && !item.Name.Contains("Minigun") && !item.Name.Contains("Chaingun") && !item.Name.Contains("Chain Gun")) {
			return false;
		}

		// Exclude non auto reuse
		if (!item.autoReuse) {
			return false;
		}

		// Exclude very fast and slower weapons
		if (item.useTime > InsanelyFast) {
			return false;
		}

		return IsGun(item) && !LaserGunCheck(item);
	}






	public static bool IsLauncher(Item item) => item.useAmmo == AmmoID.Rocket || item.useAmmo == AmmoID.JackOLantern;
	

	public static bool GrenadeLauncherCheck(Item item) {
		if (!ContentSampleUtils.TryGetProjectile(item.shoot, out var proj)) {
			return false;
		}

		if (proj.aiStyle != ProjAIStyleID.Explosive || OverhaulProjectileTags.Rocket.Has(proj.type)) {
			return false;
		}

		return IsLauncher(item);
	}

	public static bool RocketLauncherCheck(Item item) {
		if (!ContentSampleUtils.TryGetProjectile(item.shoot, out var proj)) {
			return false;
		}

		if (proj.aiStyle != ProjAIStyleID.Explosive || OverhaulProjectileTags.Grenade.Has(proj.type)) {
			return false;
		}

		return IsLauncher(item);
	}


	public static bool LaserGunCheck(Item item) => item.UseSound == SoundID.Item12 || item.UseSound == SoundID.Item33 || item.UseSound == SoundID.Item75 || item.UseSound == SoundID.Item157 || item.UseSound == SoundID.Item158;
}