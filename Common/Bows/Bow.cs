﻿using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
using TerrariaOverhaul.Common.Charging;
// DA Edit
namespace TerrariaOverhaul.Common.ModEntities.Items.Overhauls;

public partial class Bow : ItemOverhaul
{
	public static readonly SoundStyle BowFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Bows/BowFire") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};
	public static readonly SoundStyle BowChargeSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Bows/BowPull") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};
	public static readonly SoundStyle BowEmptySound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Bows/BowEmpty") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};

	public override bool ShouldApplyItemOverhaul(Item item)
	{
		// Ignore Crossbows and Repeaters
		if (item.Name.Contains("Repeater") || item.Name.Contains("Crossbow") || item.type == ItemID.ChlorophyteShotbow || item.type == ItemID.StakeLauncher) {
			return false;
		}

		// Ignore weapons that don't shoot, and ones that deal hitbox damage 
		if (item.shoot <= ProjectileID.None || !item.noMelee) {
			return false;
		}

		// Ignore weapons that don't shoot arrows.
		if (item.useAmmo != AmmoID.Arrow) {
			return false;
		}

		// Avoid tools and placeables
		if (item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.createTile >= TileID.Dirt || item.createWall >= 0) {
			return false;
		}

		return true;
	}

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

//		if (item.UseSound == SoundID.Item5) {
//			item.UseSound = BowFireSound;
//		}

		item.EnableComponent<ItemPowerAttacks>(c => {
			c.ChargeLengthMultiplier = 1.5f;
			c.CommonStatMultipliers.ProjectileDamageMultiplier = 1.5f;
			c.CommonStatMultipliers.ProjectileKnockbackMultiplier = 1.5f;
			c.CommonStatMultipliers.ProjectileSpeedMultiplier = 1.5f;
		});

		if (!Main.dedServ) {
			item.EnableComponent<ItemUseScreenShake>(c => {
				c.ScreenShake = new ScreenShake(0.3f, 0.2f);
			});

			item.EnableComponent<ItemCrosshairController>();

			item.EnableComponent<ItemPowerAttackSounds>(c => {
				c.Sound = BowChargeSound;
				c.CancelPlaybackOnEnd = true;
			});
		}
	}
}
