using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MaverickHunterClass;
using System;
using Microsoft.Xna.Framework.Graphics;
using MaverickHunterClass.Common.Players;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.DataStructures;

namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class ShadowGiga : ModProjectile
    {



        public override void SetStaticDefaults()
        {
            // Total count animation frames
            Main.projFrames[Projectile.type] = 4;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void SetDefaults()
        {
            Projectile.width = 94;
            Projectile.height = 26;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<MHunterDamage>();
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
            Projectile.velocity = Vector2.Zero;

            Projectile.aiStyle = ProjAIStyleID.GemStaffBolt;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Vector2 newCenter = new Vector2(playerCenter.X + 60 * player.direction, playerCenter.Y);
            
            if (Projectile.ai[0] <=1f)
            Projectile.NewProjectile(Projectile.InheritSource(Projectile), player.Center, Vector2.Zero, 
                    ModContent.ProjectileType<ShadowBusterShot4>(), Projectile.damage + 5, Projectile.knockBack, Main.myPlayer
                    , Projectile.ai[0]);
            Projectile.ai[0] += 1f;

            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            busterPlayer.activeBusterShots--;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            busterPlayer.isCharging = false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
         

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }
    }
}
