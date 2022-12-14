using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MaverickHunterClass;
using System;
using Microsoft.Xna.Framework.Graphics;
using MaverickHunterClass.Common.Players;
using Mono.Cecil;

namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class SpiralShot2 : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 45;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<MHunterDamage>();
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 180;

            Projectile.tileCollide = false;


            //Projectile.aiStyle = ProjectileID.Bullet;

            Projectile.aiStyle = ProjAIStyleID.GemStaffBolt;
        }
        public override void AI()
        {
            
           
           
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            switch (Projectile.ai[0])
            {
                case 0:
                    Projectile.frame = 2;
                    Projectile.ai[0] += 5;
                    break;
                case 1:
                    Projectile.frame = 6;
                    Projectile.ai[0] += 5;
                    break;
            }
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

        }
      
        public override bool PreDraw(ref Color lightColor)
        {

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            int startX = 0;
            if (Projectile.ai[1] <= 4f)
            {
                Projectile.ai[1] += 1;
                startX = 88;
            }
            else if (Projectile.ai[1] <= 8f)
            {
                Projectile.ai[1] += 1;
                startX = 62;
            }
            else if (Projectile.ai[1] <= 12f)
            {
                Projectile.ai[1] += 1;
                startX = 38;
            }
            Rectangle sourceRectangle = new Rectangle(startX, startY, texture.Width - startX, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
