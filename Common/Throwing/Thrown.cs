using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
using TerrariaOverhaul.Common.Charging;
using TerrariaOverhaul.Utilities;
// DA Addition
namespace TerrariaOverhaul.Common.ModEntities.Items.Overhauls;

public partial class Thrown : ItemOverhaul
{
	public override bool ShouldApplyItemOverhaul(Item item)
	{
		//Ignore summon weapons
		if(item.CountsAsClass(DamageClass.Summon)) {
			return false;
		}

		//Ignore weapons that aren't thrown
		if(item.useStyle != ItemUseStyleID.Swing && !(item.useStyle == ItemUseStyleID.Shoot && item.noUseGraphic)) {
			return false;
		}

		//Ignore spears and flails
		if (item.shoot > 0) {
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);
			if(projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.NorthPoleSpear || projectile.aiStyle == ProjAIStyleID.Flail) {
				return false;
			}
		}

		//Avoid tools and placeables
		if(item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.createTile >= TileID.Dirt || item.createWall >= 0) {
			return false;
		}

		//Ignore weapons that don't shoot, or deal hitbox damage
		if(item.shoot <= ProjectileID.None || !item.noMelee) {
			return false;
		}

		return true;
	}

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

		Projectile projectile = new Projectile();
		projectile.SetDefaults(item.shoot);

		item.EnableComponent<ItemPowerAttacks>(c => {
			c.ChargeLengthMultiplier = 1.5f;
			var modifiers = new CommonStatModifiers();
			if (projectile.aiStyle != ProjAIStyleID.GroundProjectile && projectile.aiStyle != ProjAIStyleID.Explosive) { // Don't power up grenades, spiky balls, and similar
				modifiers.ProjectileDamageMultiplier = 2f;
				modifiers.ProjectileKnockbackMultiplier = 1.5f;
			}
			modifiers.ProjectileSpeedMultiplier = 1.5f;
			c.StatModifiers.Single = modifiers;
		});
	}
}
