﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class GatewayBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gateway Open");
            Description.SetDefault("Ready to traverse the cosmos");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
             
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
           
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
