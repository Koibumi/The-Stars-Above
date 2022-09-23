using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class MantisCatPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			DisplayName.SetDefault("Mantis Cat"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 11;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BlackCat);
			AIType = ProjectileID.BlackCat;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			DrawOriginOffsetY = -10;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.blackCat = false; // Relic from AIType
			return true;
		}
		bool invisible;
		public override void AI()
		{
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (Projectile.velocity.Y >= 0.3 || Projectile.velocity.X >= 0.3 || Projectile.velocity.Y <= -0.3 || Projectile.velocity.X <= -0.3)
			{
				if (Projectile.frameCounter++ >= 10)
				{

					if (Projectile.frame++ > 5)
					{
						Projectile.frame = 1;
					}
				}
				if (!invisible)
                {
					//poof dust here
					invisible = true;
                }
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				//dust = Terraria.Dust.NewDustPerfect(position, 71, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);

				
			}
			else
			{
				Projectile.frame = 0;
				invisible = false;
				
				//projectile.frame = 0;
			}
			
			if (player.dead)
			{
				modPlayer.MantisCatPet = false;
			}
			if (modPlayer.MantisCatPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}