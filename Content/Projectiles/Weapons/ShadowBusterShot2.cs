using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MaverickHunterClass;
using System;
using Microsoft.Xna.Framework.Graphics;
using MaverickHunterClass.Common.Players;
using System.Runtime.InteropServices;
using Terraria.DataStructures;

namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class ShadowBusterShot2 : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 198;
            Projectile.height = 130;

            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<MHunterDamage>();
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 48;
            Projectile.tileCollide = false;


            //Projectile.aiStyle = ProjectileID.Bullet;

            Projectile.aiStyle = ProjAIStyleID.GemStaffBolt;

          
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Projectile.velocity,
                ModContent.ProjectileType<ShadowBusterShot3>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
        }

        public override void AI()
        {
           
            base.AI();
            
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Vector2 newCenter = new Vector2(playerCenter.X + 20 * player.direction, playerCenter.Y);
            Projectile.Center = newCenter;


            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            busterPlayer.activeBusterShots--;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (player.direction == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
