using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class DuckHuntBirdPetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Hunted Duck");
			Description.SetDefault("It seems the name has become outdated");
			Main.buffNoTimeDisplay[Type] = true;
			//Main.pet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<WeaponPlayer>().DuckHuntBirdPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.DuckHuntBirdPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.DuckHuntBirdPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			
			
		}
	}
}