using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using MaverickHunterClass.Content.Projectiles.Weapons;
using MaverickHunterClass.Common.Players;

namespace MaverickHunterClass.Content.Items.Weapons
{
    internal class FirstBuster : ModItem
    {
        

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
           
            Item.DamageType = ModContent.GetInstance<MHunterDamage>();
            Item.noMelee = true;

            Item.damage = 0;
            Item.knockBack = 3.2f;

            Item.useTime = 15;
            Item.useAnimation = 10;

            Item.channel = true;

            //Item.UseSound = SoundID.Item71;

            Item.shoot = ModContent.ProjectileType<FirstCharge>();
            Item.shootSpeed = 1f;
            

            
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            if (!busterPlayer.isCharging && busterPlayer.activeBusterShots < busterPlayer.maxBusterShots)
            {
                busterPlayer.isCharging = true;
                   busterPlayer.activeBusterShots++;
                return true;
            }
            return false;
        }

    }
}
