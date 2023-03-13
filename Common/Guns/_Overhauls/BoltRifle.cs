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

public class BoltRifle : ItemOverhaul
{
	public static readonly SoundStyle AssaultRifleFireSound = new SoundStyle($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/AssaultRifle/AssaultRifleFire", 3) {
		Volume = 0.125f,
		PitchVariance = 0.2f,
	};
	public static readonly SoundStyle HandgunFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/Handgun/HandgunFire") {
		Volume = 0.15f,
		PitchVariance = 0.2f,
	};
	public static readonly SoundStyle ShotgunPumpSound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/Shotgun/ShotgunPump") {
		Volume = 0.25f,
		PitchVariance = 0.1f
	};
	public static readonly SoundStyle BoltRifleFireSound = new SoundStyle($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/BoltRifle/BoltRifleFire") {
		Volume = 0.3f,
		PitchVariance = 0.2f,
	};
	public static readonly SoundStyle BoltRiflePumpSound = new SoundStyle($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/BoltRifle/BoltRiflePump") {
		Volume = 0.2f,
		PitchVariance = 0.1f,
	};

	private uint pumpTime;

	public SoundStyle? PumpSound { get; set; }
	public int ShellCount { get; set; } = 1;

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.BoltRifleCheck(item);

	public override void SetDefaults(Item item)
	{
		if (item.type == ItemID.RedRyder) item.UseSound = HandgunFireSound;
		else if (item.UseSound == SoundID.Item40) item.UseSound = BoltRifleFireSound;
		else item.UseSound = AssaultRifleFireSound;
		if (item.type == ItemID.RedRyder) PumpSound = ShotgunPumpSound;
		else PumpSound = BoltRiflePumpSound;

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();

			item.EnableComponent<ItemBulletCasings>(c => {
				c.CasingGoreType = ModContent.GoreType<BulletCasing>();
				c.SpawnOnUse = false;
			});
		}
	}

	public override bool? UseItem(Item item, Player player)
	{
		bool? baseResult = base.UseItem(item, player);

		if (baseResult == false) {
			return false;
		}

		pumpTime = (uint)(Main.GameUpdateCount + player.itemAnimationMax / 2);

		return baseResult;
	}

	public override void HoldItem(Item item, Player player)
	{
		base.HoldItem(item, player);

		if (!Main.dedServ && PumpSound != null && pumpTime != 0 && Main.GameUpdateCount == pumpTime) {
			SoundEngine.PlaySound(PumpSound, player.Center);

			item.GetGlobalItem<ItemBulletCasings>().SpawnCasings(item, player);
		}
	}
}
