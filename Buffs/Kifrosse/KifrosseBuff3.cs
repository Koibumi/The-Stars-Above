using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
    public class KifrosseBuff3 : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Kifrosse (3 Tails)");
			Description.SetDefault("Mystic energy chills the air around you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.Kifrosse3>()] > 0) {
				modPlayer.Kifrosse3 = true;
			}
			if (!modPlayer.Kifrosse3) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
	}
}