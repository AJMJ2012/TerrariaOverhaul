using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Camera;
using TerrariaOverhaul.Common.Crosshairs;
using TerrariaOverhaul.Common.Items;
using TerrariaOverhaul.Common.Movement;
using TerrariaOverhaul.Common.Recoil;
using TerrariaOverhaul.Content.Gores;
using TerrariaOverhaul.Core.ItemComponents;
using TerrariaOverhaul.Core.ItemOverhauls;
using TerrariaOverhaul.Core.Time;
using TerrariaOverhaul.Utilities;
// DA Edit
namespace TerrariaOverhaul.Common.Guns;

public class Minigun : ItemOverhaul
{
	public static readonly SoundStyle MinigunFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/Minigun/MinigunFire") {
		Volume = 0.15f,
		PitchVariance = 0.2f,
	};

	private float speedFactor;

	public virtual float MinSpeedFactor => 0.333f;
	public virtual float AccelerationTime => 1f;
	public virtual float DecelerationTime => 1f;

	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.MinigunCheck(item);

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);

		speedFactor = MinSpeedFactor;

		item.UseSound = MinigunFireSound;

		item.EnableComponent<ItemUseVelocityRecoil>(e => {
			e.BaseVelocity = new(4.0f, 20.85f);
			e.MaxVelocity = new(3.0f, 5.0f);
		});

		if (!Main.dedServ) {
			item.EnableComponent<ItemAimRecoil>();
			item.EnableComponent<ItemPlaySoundOnEveryUse>();

			item.EnableComponent<ItemBulletCasings>(c => {
				c.CasingGoreType = ModContent.GoreType<BulletCasing>();
			});
		}
	}

	public override float UseSpeedMultiplier(Item item, Player player)
	{
		return base.UseSpeedMultiplier(item, player) * speedFactor;
	}

	public override void HoldItem(Item item, Player player)
	{
		base.HoldItem(item, player);

		if (player.controlUseItem) {
			speedFactor = MathUtils.StepTowards(speedFactor, 1.25f, AccelerationTime * TimeSystem.LogicDeltaTime);
		} else {
			speedFactor = MinSpeedFactor; //speedFactor = MathUtils.StepTowards(speedFactor, MinSpeedFactor, DecelerationTime * TimeSystem.LogicDeltaTime);
		}
	}
}
