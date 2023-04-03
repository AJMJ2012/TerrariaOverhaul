using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Charging;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
using TerrariaOverhaul.Utilities;
// DA Edit
namespace TerrariaOverhaul.Common.ModEntities.Items.Overhauls;

public partial class Bow : ItemOverhaul
{
	public static readonly SoundStyle FireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Bows/BowFire") {
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
//			item.UseSound = FireSound;
//		}

		item.EnableComponent<ItemPowerAttacks>(c => {
			c.ChargeLengthMultiplier = 1.5f; // x2.5

			var modifiers = new CommonStatModifiers();

			modifiers.ProjectileDamageMultiplier = modifiers.MeleeDamageMultiplier = 1.5f;
			modifiers.ProjectileKnockbackMultiplier = modifiers.MeleeKnockbackMultiplier = 1.5f;
			modifiers.ProjectileSpeedMultiplier = 1.5f;

			c.StatModifiers.Single = modifiers;
		});

		if (!Main.dedServ) {
			item.EnableComponent<ItemPowerAttackSounds>(c => {
				c.Sound = ChargeSound;
			});
		}
	}
}
