using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using MaverickHunterClass.Content.Projectiles.Weapons;
using MaverickHunterClass.Common.Players;


namespace MaverickHunterClass.Content.Items.Weapons
{
    internal class TutorialWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.DamageType = ModContent.GetInstance<MHunterDamage>();
            Item.noMelee = true;
            
            Item.damage = 24;
            Item.knockBack = 3.2f;

            Item.useTime = 6;
            Item.useAnimation = 6;

            Item.channel = true;

            Item.UseSound = SoundID.Item71;

            Item.shoot = ModContent.ProjectileType<SpiralShot>();
            Item.shootSpeed = 5f;
            
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            /*BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            if (!busterPlayer.isCharging && busterPlayer.activeBusterShots < busterPlayer.maxBusterShots)
            {
                busterPlayer.isCharging = true;
                busterPlayer.activeBusterShots++;
                return true;
            }*/
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<SpiralShot>(), damage, knockback, player.whoAmI,0f,2f);
            return false;
        }
    }
}
