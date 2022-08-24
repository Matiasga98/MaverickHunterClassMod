﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaverickHunterClass.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace MaverickHunterClass.Content.Items.Accesories
{
    public class Rapid5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to have more bullets on screen");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.maxStack = 1;
            //Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Set the HasExampleImmunityAcc bool to true to ensure we have this accessory
            // And apply the changes in ModPlayer.PostHurt correctly
            player.GetModPlayer<BusterPlayer>().maxBusterShots = 5;
        }
    }
}