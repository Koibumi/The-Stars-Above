using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SakuraVengeance
{
    public class SakuraSlashCooling1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sakura's Vengeance");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			//DrawOffsetX = 40;
			//DrawOriginOffsetY = 81;
		}

		public override void SetDefaults() {
			Projectile.width = 240;               //The width of projectile hitbox
			Projectile.height = 240;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 25;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		int direction;//0 is right 1 is left
		float rotationStrength = 0.1f;
		bool firstSpawn = true;
		double deg;
		public override void AI()
		{
			
			Player player = Main.player[Projectile.owner];
			player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

			if (firstSpawn)
            {
				Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y-player.Center.Y,Main.MouseWorld.X-player.Center.X) - 30);
				firstSpawn = false;
            }
			if (player.dead && !player.active)
			{
				Projectile.Kill();
			}
			if(Projectile.timeLeft > 20)
            {
				rotationStrength += 0.8f;
			}
			else
            {
				rotationStrength -= 1.2f;
				if(rotationStrength < -12f)
                {
					rotationStrength = -12f;
                }
				Projectile.alpha += 5;
			}
			
			
			
			deg = Projectile.ai[1] += 12f + rotationStrength;

			
			
			double rad = deg * (Math.PI / 180);
			double dist = 108;

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			Projectile.rotation = Vector2.Normalize(Main.player[Projectile.owner].Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);

			
			
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int randomPetals = Main.rand.Next(1, 4);
			for (int i = 0; i < randomPetals; i++)
			{
				// Random upward vector.
				Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-1, -4));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, vel, ProjectileType<SakuraPetal>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);
			}
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 92, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.4f);

			}
			target.AddBuff(BuffID.Frostburn, 120);
			Player player = Main.player[Projectile.owner];

			if (!target.active)//On kill
			{
				player.GetModPlayer<WeaponPlayer>().VengeanceGauge +=  8;
				if (player.GetModPlayer<WeaponPlayer>().VengeanceGauge >= 100)
				{
					player.GetModPlayer<WeaponPlayer>().VengeanceGauge = 100;

				}
			}
			if (player.HasBuff(BuffType<Buffs.SakuraVengeance.ElementalChaos>()))
			{
				target.AddBuff(BuffID.Frostburn, 120);
				target.AddBuff(BuffID.OnFire, 120);
				target.AddBuff(BuffID.Ichor, 120);
				target.AddBuff(BuffID.CursedInferno, 120);
			}
		}

	}
}
