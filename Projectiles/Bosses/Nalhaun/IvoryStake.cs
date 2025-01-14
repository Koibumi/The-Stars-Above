using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class IvoryStake : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ivory Stake");     //The English name of the projectile

			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults() {
			Projectile.width = 32;               //The width of projectile hitbox
			Projectile.height = 32;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			  
			//Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

			

		}
		bool firstSpawn = true;
		float randomRotate;
        public override void AI()
        {
			DrawOffsetX = -90;
			if(firstSpawn)
            {
				Projectile.frame = Main.rand.Next(0, 6);
				//randomRotate = Main.rand.NextFloat(-4, 4);
				firstSpawn = false;
            }
			Projectile.alpha+=1;
			//projectile.scale -= 0.03f;
			float rotationsPerSecond = 0f + randomRotate;
			bool rotateClockwise = true;
			//The rotation is set here
			//Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			Projectile.rotation += MathHelper.ToRadians(-90);
			if (Projectile.alpha > 250 || Projectile.scale < 0.3f)
            {
				Projectile.Kill();
            }

            base.AI();
        }
        

		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
			// Play explosion sound
			
			// Large Smoke Gore spawn
			

		}
	}
}
