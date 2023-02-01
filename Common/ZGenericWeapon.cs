using Terraria;
using Terraria.ID;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
// DA Creation
namespace TerrariaOverhaul.Common.ModEntities.Items.Overhauls;

public partial class ZGenericWeapon : ItemOverhaul
{
	public override bool ShouldApplyItemOverhaul(Item item)
	{
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

		if (!Main.dedServ) {
			item.EnableComponent<ItemCrosshairController>();
		}
	}

}
