using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.GoldenKatana
{
    //
    public class GoldenKatanaChargeAttack3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurum Edge");
			Main.projFrames[Projectile.type] = 3;
			
			//DrawOffsetX = -60;
		}
		
		public override void SetDefaults()
		{
			Projectile.width = 262;
			Projectile.height = 262;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 45;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;

			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = ModContent.GetInstance<Systems.AuricDamageClass>();
			Projectile.tileCollide = false;
			Projectile.friendly = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			DrawOriginOffsetY = -20;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
			 
			 
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 7f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor -= 0f;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 0.7f;
				}
			}
			if (Projectile.frame >= 2)
			{
				Projectile.alpha += 8;
			}

			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 2)
				{
					Projectile.frame++;
				}
				else
				{
					Projectile.friendly = false;
				}

			}
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
			if (projOwner.GetModPlayer<WeaponPlayer>().bowCharge > 0)
			{
				Projectile.Kill();
			}

			Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
			// Adding Pi to rotation if facing left corrects the drawing
			Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);


			// These dusts are added later, for the 'ExampleMod' effect
			/*if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player projOwner = Main.player[Projectile.owner];
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.PinkCrystalShard, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
			}
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.PinkFairy, 0f + Main.rand.Next(-22, 22), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
			}
			for (int d = 0; d < 32; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-4, 4), 150, default(Color), 1.1f);
			}
			for (int d = 0; d < 5; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 7, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 6; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 269, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 7; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 78, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default(Color), 1.5f);
			}
			// Smoke Dust spawn
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			

			target.AddBuff(BuffType<Hyperburn>(), 240);
			SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceGreatsword, target.Center);

			base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			crit = true;

            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}