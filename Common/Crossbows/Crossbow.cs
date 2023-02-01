using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Common.Items;
// DA Creation
namespace TerrariaOverhaul.Common.Crossbow
{
	public class Crossbow : ItemOverhaul
	{
		public static readonly SoundStyle CrossbowFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/DA/Sounds/Items/Crossbows/CrossbowsShoot", 4) {
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
			=> item.Name.Contains("Repeater") || item.Name.Contains("Crossbow") || item.type == ItemID.ChlorophyteShotbow || item.type == ItemID.StakeLauncher;

		public override void SetDefaults(Item item)
		{

		//	if (item.UseSound == SoundID.Item5) {
		//		item.UseSound = CrossbowFireSound;
		//	}
			ReloadSound = CrossbowReloadSound;

			if (!Main.dedServ) {
				item.EnableComponent<ItemAimRecoil>();
				item.EnableComponent<ItemCrosshairController>();

				item.EnableComponent<ItemUseVisualRecoil>(c => {
					c.Power = 5f;
				});

				item.EnableComponent<ItemUseScreenShake>(c => {
					c.ScreenShake = new ScreenShake(2f, 0.2f);
				});
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
}
