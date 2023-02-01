using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Common.Items;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Content.Gores;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
// DA Creation
namespace TerrariaOverhaul.Common.Guns;

public class Nailgun : ItemOverhaul
{
	public static readonly SoundStyle NailgunFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Nailgun/NailgunFire") {
		Volume = 0.2f,
		PitchVariance = 0.2f,
	};

	public override bool ShouldApplyItemOverhaul(Item item)
		=> item.useAmmo == AmmoID.NailFriendly;

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

		item.UseSound = NailgunFireSound;

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemMuzzleflashes>();
			item.EnableComponent<ItemCrosshairController>();
			item.EnableComponent<ItemPlaySoundOnEveryUse>();

			item.EnableComponent<ItemUseVisualRecoil>(c => {
				c.Power = 10f;
			});

			item.EnableComponent<ItemUseScreenShake>(c => {
				c.ScreenShake = new ScreenShake(0.45f, 0.2f);
			});
		}
	}
}
