﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class InnerAgony : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Inner Agony");
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}
		bool onSpawn = true;
        public override bool PreAI()
        {
			

			return true;
        }
        public override bool CanHitPlayer(Player target)
        {
			//Within the arena.
			if (target.Distance(Projectile.Center) < 1000)
            {
				//This is Inner Agony, so you need to run away from the middle.
				if (target.Distance(Projectile.Center) < 350)
				{
					if(Projectile.ai[0] <= 0)//Timer for damage.
                    {
						
						return true;
						
                    }
					else
                    {
						return false;
                    }
				}
			}
				
	

			return false;
        }
        public override void AI() {

			Projectile.timeLeft = 10;

			for (int i = 0; i < 10; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 350);
				offset.Y += (float)(Math.Cos(angle) * 350);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Flare, Projectile.velocity, 20, default(Color), 0.5f);

				d.fadeIn = 1f;
				d.noGravity = true;
			}
			if(Projectile.ai[0] < 60)
            {
				for (int i = 0; i < 50; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 350);
					offset.Y += (float)(Math.Cos(angle) * 350);

					Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.LifeDrain, Projectile.velocity, 20, default(Color), 1.5f);

					d.fadeIn = 1f;
					d.noGravity = true;
				}
			}


			Projectile.ai[0]--;
			if(Projectile.ai[0] == 0)
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_Laevateinn, Projectile.Center);

				for (int i = 0; i < 45; i++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-18, 18), 0f + Main.rand.Next(-18, 18), 150, default(Color), 1.7f);

				}
				for (int i = 0; i < 53; i++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.Flare, 0f + Main.rand.Next(-28, 28), 0f + Main.rand.Next(-28, 28), 150, default(Color), 3.7f);

				}
				for (int i = 0; i < 43; i++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-18, 18), 0f + Main.rand.Next(-18, 18), 150, default(Color), 2.7f);

				}
			}
			if(Projectile.ai[0] <= -10)
            {
				Projectile.Kill();
			}
		}
	}
}
