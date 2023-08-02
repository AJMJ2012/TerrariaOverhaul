using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;

namespace TerrariaOverhaul.Common.Archery;

public partial class Crossbow : ItemOverhaul
{
	public static readonly SoundStyle CrossbowFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Crossbows/CrossbowsShoot") {
		Volume = 0.3f,
		PitchVariance = 0.2f
	};
	public static readonly SoundStyle CrossbowReloadSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Crossbows/CrossbowReload") {
		Volume = 0.3f,
		PitchVariance = 0.1f
	};

	private uint reloadTime;

	public SoundStyle? ReloadSound { get; set; }

	public override bool ShouldApplyItemOverhaul(Item item)
	{
		return ArcheryWeapons.IsArcheryWeapon(item, out var kind) && kind == ArcheryWeapons.Kind.Repeater;
	}

	public override void SetDefaults(Item item)
	{
	//	if (item.UseSound == SoundID.Item5) {
	//		item.UseSound = CrossbowFireSound;
	//	}
		ReloadSound = CrossbowReloadSound;

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();
		}
	}

	public override bool? UseItem(Item item, Player player)
	{
		bool? baseResult = base.UseItem(item, player);

		if (baseResult == false) {
			return false;
		}

		reloadTime = (uint)(Main.GameUpdateCount + Math.Max(1, player.itemAnimationMax - 20));

		return baseResult;
	}

	public override void HoldItem(Item item, Player player)
	{
		base.HoldItem(item, player);

		if (!Main.dedServ && ReloadSound != null && reloadTime != 0 && Main.GameUpdateCount == reloadTime) {
			SoundEngine.PlaySound(ReloadSound, player.Center);
		}
	}
}