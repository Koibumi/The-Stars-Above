﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SaltwaterScourge
{
    public class CannonfireDeluge2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cannonfire Deluge (2 Stacks)");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

    }
}
