﻿using Terraria;
using Terraria.Audio;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Common.Items;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
// DA Edit
namespace TerrariaOverhaul.Common.Guns;

public class GrenadeLauncher : ItemOverhaul
{
	public static readonly SoundStyle GrenadeLauncherFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/GrenadeLauncher/GrenadeLauncherFire") {
		Volume = 0.15f,
		PitchVariance = 0.2f,
	};

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.GrenadeLauncherCheck(item);

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

		item.UseSound = GrenadeLauncherFireSound;

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();
		}
	}
}
