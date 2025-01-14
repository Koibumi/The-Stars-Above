﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
	public class TsukiVoiceOfTheOutbreak2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tsukiyomi, the First Starfarer");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 270;
			Projectile.height = 270;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.timeLeft = 160;
			Projectile.hide = false;
			Projectile.light = 1f;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
		}
		bool altFire = false;
		
		public override void AI() {

			//DrawOriginOffsetY = -90;
			Projectile.timeLeft = 10;
			Projectile.alpha -= 10;
			Projectile.ai[0]--;
			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);
			if (Projectile.ai[0] <= 120)
			{
				//Projectile.velocity.Y -= 0.5f;
				//Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.X, -80, 0);

				Projectile.ai[1]++;
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1f);

				
			}
			else
            {
				for (int i = 0; i < 5; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.FireworkFountain_Blue, 0, 0, 255,
						new Color(1f, 1f, 1f), 0.5f)];
					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}

				Projectile.position.Y = targetCenter.Y - 120;

			}

			
			if (Projectile.ai[1] >= 15)
			{
				if (foundTarget)
				{
					
					if (altFire)
                    {
						
						Projectile.ai[1] = 0;
						int type = ModContent.ProjectileType<TsukiWhisperRound>();
						SoundEngine.PlaySound(StarsAboveAudio.SFX_WhisperShot, Projectile.Center);


						Vector2 position = Projectile.Center;



						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, 40, 0, type, Projectile.damage, 0f, Main.myPlayer);
						for (int d = 0; d < 10; d++)
						{
							Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(0, 20), 0f + Main.rand.Next(0, 20), 150, default(Color), 1.5f);
						}
						for (int d = 0; d < 10; d++)
						{
							Dust.NewDust(Projectile.Center, 0, 0, DustID.WhiteTorch, 0f + Main.rand.Next(0, 20), 0f + Main.rand.Next(0, 20), 150, default(Color), 1.5f);
						}
						altFire = false;

						if (foundTarget)
						{
							for (int d = 0; d < 50; d++)
							{
								Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
							}
							Projectile.position.Y = targetCenter.Y - 120;
							for (int d = 0; d < 50; d++)
							{
								Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
							}
							
						}
					}
					else
                    {
						Projectile.ai[1] = 0;
						int type = ModContent.ProjectileType<TsukiOutbreakRound>();
						SoundEngine.PlaySound(StarsAboveAudio.SFX_outbreakShoot, Projectile.Center);


						Vector2 position = Projectile.Center;


						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 17, 20, 0, ProjectileType<TsukiTrackingBoltStrong2>(), Projectile.damage, 0f, Main.myPlayer);

						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 17, 20, 0, type, Projectile.damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X - 80, position.Y - 17, 19, 0, type, Projectile.damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X - 140, position.Y - 17, 18, 0, type, Projectile.damage, 0f, Main.myPlayer);

						for (int d = 0; d < 20; d++)
						{
							Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(0, 20), 0f + Main.rand.Next(0, 20), 150, default(Color), 1.5f);
						}
						altFire = true;

						
					}

					
					

				}


			}

			if (Projectile.ai[0] <= 0)
            {
				Projectile.Kill();
            }

			
		}
		private void SearchForTargets(out bool foundTarget, out Vector2 targetCenter)
		{


			targetCenter = Projectile.position;
			foundTarget = false;



			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.active && npc.type == ModContent.NPCType<TsukiyomiBoss>() || npc.type == ModContent.NPCType<TsukiyomiBoss>())//temp
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			//Projectile.friendly = foundTarget;
		}
	}
}
