﻿using Terraria;
using Terraria.Audio;
using Terraria.ID;
// DA Edit
namespace TerrariaOverhaul.Common.Archery;

public static class ArcheryWeapons
{
	public enum Kind
	{
		Undefined,
		Bow,
		Repeater,
	}

	public static readonly SoundStyle FireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Bows/BowShoot") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};
	public static readonly SoundStyle ChargeSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Bows/BowPull") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};
	public static readonly SoundStyle EmptySound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Bows/BowEmpty") {
		Volume = 0.375f,
		PitchVariance = 0.2f,
		MaxInstances = 3,
	};

	public static bool IsArcheryWeapon(Item item, out Kind kind)
	{
		kind = Kind.Undefined;

		// Ignore weapons that don't shoot, and ones that deal hitbox damage 
		if (item.shoot <= ProjectileID.None || !item.noMelee) {
			return false;
		}

		// Ignore channeled weapons.
		if (item.channel) {
			return false;
		}

		// Ignore weapons that don't shoot arrows.
		if (item.useAmmo != AmmoID.Arrow && item.type != ItemID.StakeLauncher) {
			return false;
		}

		// Ignore specific weapons.
		if (item.type == ItemID.FairyQueenRangedItem) {
			return false;
		}

		// Avoid tools and placeables
		if (item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.createTile >= TileID.Dirt || item.createWall >= 0) {
			return false;
		}

		kind = GetKind(item);

		return true;
	}

	private static Kind GetKind(Item item)
	{
		// Tons of items have incorrect width/height values. This is why we can't have nice things.
		return item.type switch {
			ItemID.PulseBow => Kind.Bow,
			ItemID.Tsunami => Kind.Bow,
			ItemID.FairyQueenRangedItem => Kind.Bow,
			_ when item.width > item.height => Kind.Repeater,
			_ => Kind.Bow,
		};
	}
}
