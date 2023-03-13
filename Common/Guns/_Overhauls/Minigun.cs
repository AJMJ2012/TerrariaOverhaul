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

	public override bool? UseItem(Item item, Player player)
	{
		ApplyVelocityRecoil(item, player);

		return base.UseItem(item, player);
	}

	private static void ApplyVelocityRecoil(Item item, Player player)
	{
		var mouseWorld = player.GetModPlayer<PlayerDirectioning>().MouseWorld;
		var direction = (player.Center - mouseWorld).SafeNormalize(default);
		var modifiedDirection = new Vector2(direction.X, direction.Y * Math.Abs(direction.Y));
		var velocity = modifiedDirection * new Vector2(item.useTime / 15f, item.useTime / 2.875f);

		// Disable horizontal velocity recoil whenever the player is holding a directional key opposite to the direction of the dash.
		if (Math.Sign(player.KeyDirection().X) == -Math.Sign(velocity.X)) {
			velocity.X = 0f;
		}

		// Disable vertical velocity whenever aiming upwards or standing on the ground
		if (velocity.Y > 0f || player.velocity.Y == 0f) {
			velocity.Y = 0f;
		}

		VelocityUtils.AddLimitedVelocity(player, velocity, new Vector2(3f, 5f));
	}
}
