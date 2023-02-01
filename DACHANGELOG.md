# Changelog:

**Edit 1:**

*Edits:*
* ItemAutoreuse.cs:
	* Disallow Item Autoreuse from affecting bullet weapons and fishing rods
* Bow.cs
	* Changed bow sounds for personal preference
	* Disabled changing of vanilla bow shoot sound
	* Added missing power attacks
	* Ignores crossbows and repeaters
	* New sounds sourced from Morrowind
* ScreenShakeSystem.cs
	* Reduced overall screen shake
* CrosshairSystem.cs
	* Disabled crosshair if smart cursor is enabled
	* Disabled crosshair if the mouse has text which prevents overlapping
	* Added support for the Rainbow Cursor vanity item
* AssaultRifle.cs
	* Checks now refer to GunChecks.AssaultRifleCheck
* GrenadeLauncher.cs
	* Checks now refer to GunChecks.GrenadeLauncherCheck
	* Changed screen shake to reflect other weapon changes (Mirsario missed it)
* Handgun.cs
	* Checks now refer to GunChecks.HandgunCheck
* Minigun.cs
	* Checks now refer to GunChecks.MinigunCheck
	* Miniguns fire up to 25% faster
* RocketLauncher.cs
	* Checks now refer to GunChecks.RocketLauncherCheck
* MagicWeapon.cs
	* Ignored additional sounds Item12 and Item33
	* Disabled changing of vanilla firing sound
	* Decreased charge length, projectile damage, and projectile speed multipliers to 1.5
* MainMenuOverlay.cs
	* Added text to show you're using this modified version
* SlashPlayerDrawLayer.cs
	* Disabled slashes if using mods Melee Weapon Effects or Armament Display
* MeleeAnimation.cs
	* Disabled enhanced animations if using mods Melee Weapon Effects or Armament Display
* Axe.cs
	* Swapped quick slash animation for the regular slash animation
	* Increased power attack range multiplaier to 1.5
* Broadsword.cs
	* Increased power attack range multiplaier to 1.5
* Hammer.cs
	* Swapped quick slash animation for the regular slash animation
	* Increased power attack range multiplaier to 1.5
* PlayerHoldOutAnimation.cs
	* Disabled hold out animation if using mods Weapon Out or Weapon Out Lite
* ProjectileExplosionEffect.cs
	* Added calls to play new explosion sounds
		* Sounds are currently overlayed over vanilla
* ItemAimRecoil.cs
	* Fixed recoil applying to other players
		* Holdover from old edit, it may be fixed elsewhere now
* OverhaulProjectileTags.cs
	* Added a bunch more stuff
* build.txt
	* Added text to show you're using this modified version


*Additions:*
* Added crossbow overhaul (Crossbow.cs)
	* Crossbows do not have power attacks and have a reload sound mid use
	* Sounds sourced from Morrowind
* Added bolt rifle overhaul (BoltRifle.cs)
	* Bolt rifles are slower than assult rifles and have a bolt action sound mid use
	* Red Ryder use the handgun firing sound and shotgun pump sound
	* Rifles that use the Item40 sound use a new heavier firing sound
	* All other rifles use the assult rifle firing sound
	* New sounds sourced from a Fallout New Vegas mod: Improved Sounds FX
* Added laser gun overhaul (LaserGun.cs)
	* Laser guns are treated like other guns
	* Laser guns have new firing sounds
	* Sounds sourced from Noita
* Added Nailgun overhaul (Nailgun.cs)
	* Nailguns have new firing sounds
	* Sounds sourced from Quake 1.5
* Added throwing weapon overhaul (Thrown.cs)
	* Throwing weapons have power attacks
	* Spiky Balls, Grenades, and similar will not have increased knockback and damage from power attacks
* Added generic weapon overhaul (ZGenericWeapon.cs)
	* This is just to show the combat crosshair for all other weapons
* Gun Checks (GunChecks.cs)
	* Contains all the checks for guns
	* Various weapons have had their checks modified to fit a better range of items and reduce conflicts
* New Explosion Sounds (ProjectileExplosionSounds.cs)
	* Added various different explosion sounds for different types and sizes of explosions
	* Sounds sourced from Noita
* New Laser Hit Sounds (ProjectileLaserHitSounds.cs)
	* Added new hit sounds for laser projectiles
	* Sounds sourced from Noita
* New Magic Hit Sounds (ProjectileMagicHitSounds.cs)
	* Added new hit sounds for magical projectiles
	* Sounds sourced from Noita