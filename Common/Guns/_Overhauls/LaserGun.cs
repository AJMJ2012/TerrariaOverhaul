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

public class LaserGun : ItemOverhaul
{
	public bool overhaulApplied = false;

	public static readonly SoundStyle FireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/LaserGun/laser_small") {
		Volume = 0.3f,
		PitchVariance = 0.2f,
	};

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.LaserGunCheck(item);

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

		item.UseSound = FireSound;

		overhaulApplied = true;

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemPlaySoundOnEveryUse>();
		}
	}
}
