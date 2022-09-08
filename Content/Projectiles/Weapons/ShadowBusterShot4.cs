using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MaverickHunterClass;
using System;
using Microsoft.Xna.Framework.Graphics;
using MaverickHunterClass.Common.Players;
using Terraria.DataStructures;

namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class ShadowBusterShot4 : ModProjectile
    {
        public ref float SpinningStateTimer => ref Projectile.localAI[1];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 10;
        }

        public override void SetDefaults()
        {
            Projectile.width = 156;
            Projectile.height = 148;

            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<MHunterDamage>();
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            DrawHeldProjInFrontOfHeldItemAndArms = true;


            //Projectile.aiStyle = ProjectileID.Bullet;

            Projectile.aiStyle = ProjAIStyleID.GemStaffBolt;

          
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center + new Vector2(1,0) * 30f;
        }

        public override void AI()
        {
            DrawHeldProjInFrontOfHeldItemAndArms = true;
            base.AI();
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            
            
            var rotationDirection = Projectile.ai[0] == 0 ? 1:-1;
            Vector2 offsetFromPlayer = new Vector2(1,0).RotatedBy((float)Math.PI * 1f * (SpinningStateTimer / 60f) * rotationDirection);
            SpinningStateTimer += 1f;
            Projectile.Center = player.Center + offsetFromPlayer * 80f;
         
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.ai[1] += 1f;
                float rotation = Projectile.ai[1] % 4;
                Projectile.rotation =  rotation * (float)(Math.PI/2);
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                    Projectile.frame = 2;
            }
            if (Projectile.ai[1] == 29f)
            {
                Projectile.frame = 8;
            }

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
