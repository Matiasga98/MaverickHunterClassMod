using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MaverickHunterClass;
using System;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using MaverickHunterClass.Common.Players;
using Terraria.DataStructures;

namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class SecondCharge : ModProjectile
    {

        private enum AIState
        {
            Charging,
            Shooting,
        }

        private int charge = 0;
        private int chargeLimit = 160;
        private bool fullCharge = false;
      
        private AIState CurrentAIState
        {
            get => (AIState)Projectile.ai[0];
            set => Projectile.ai[0] = (float)value;
        }

        public ref float SpinningStateTimer => ref Projectile.localAI[1];
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            Projectile.width = 0;
            Projectile.height = 0;

            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<MHunterDamage>();
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;

            
                      

            //Projectile.aiStyle = ProjectileID.Bullet;

            Projectile.aiStyle = ProjAIStyleID.GemStaffBolt;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            charge = busterPlayer.initialCharge;
            CurrentAIState = charge>=chargeLimit? AIState.Shooting: AIState.Charging;
        }

        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            BusterPlayer busterPlayer = player.GetModPlayer<BusterPlayer>();
            

            //base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
            

            Vector2 mountedCenter = player.MountedCenter;
            float launchSpeed = 5f;

            
            switch (CurrentAIState)
            {
                case AIState.Charging:
                    {
                        busterPlayer.isCharging = true;
                        Vector2 unitVectorTowardsMouse = mountedCenter.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * player.direction);
                        player.ChangeDir((unitVectorTowardsMouse.X > 0f).ToDirectionInt());
                        if (!player.channel)
                        {
                            //busterPlayer.isCharging = false;
                            CurrentAIState = AIState.Shooting;
                            Projectile.velocity = unitVectorTowardsMouse * launchSpeed;// + player.velocity;
                        }
                        else
                        {
                            if (charge < chargeLimit)
                            {
                                charge += busterPlayer.chargeSpeed;
                            }
                            else
                            {
                                fullCharge = true;
                            }
                            Projectile.Center = mountedCenter;
                            Projectile.velocity = Vector2.Zero;

                            if (++Projectile.frameCounter >= 8)
                            {
                                Projectile.frameCounter = 0;
                                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                                    Projectile.frame = 0;
                            }
                        }
                        break;
                    }
                case AIState.Shooting:
                    {
                        Vector2 unitVectorTowardsMouse = mountedCenter.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * player.direction);
                        Projectile.velocity = unitVectorTowardsMouse * launchSpeed;
                        if (charge < 20)
                        {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                Vector2 newCenter = new Vector2(Projectile.Center.X + 15, Projectile.Center.Y);
                                Projectile.NewProjectile(Projectile.InheritSource(Projectile), newCenter, Projectile.velocity, ModContent.ProjectileType<FirstBuster1>(), Projectile.damage + 5, Projectile.knockBack, Main.myPlayer);
                                busterPlayer.isCharging = false;
                                Projectile.Kill();
                            }
                        }
                        else if (charge < 100)
                        {

                            if (Main.myPlayer == Projectile.owner)
                            {
                                Vector2 newCenter = new Vector2(Projectile.Center.X + 27, Projectile.Center.Y);
                                Projectile.NewProjectile(Projectile.InheritSource(Projectile), newCenter, Projectile.velocity, ModContent.ProjectileType<FirstBuster2>(), Projectile.damage + 10, Projectile.knockBack, Main.myPlayer);
                                busterPlayer.isCharging = false;
                                Projectile.Kill();
                            }

                        }
                        else if (charge < chargeLimit)
                        {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                Vector2 newCenter = new Vector2(Projectile.Center.X + 61, Projectile.Center.Y);
                                Projectile.NewProjectile(Projectile.InheritSource(Projectile), newCenter, Projectile.velocity * 1.1f, ModContent.ProjectileType<SecondBuster1>(), Projectile.damage + 30, Projectile.knockBack, Main.myPlayer);
                                busterPlayer.isCharging = false;
                                Projectile.Kill();
                            }
                        }
                        else
                        {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                //Vector2 newCenter = new Vector2(Projectile.Center.X + 61, Projectile.Center.Y);
                                Projectile.NewProjectile(Projectile.InheritSource(Projectile), player.Center, Projectile.velocity * 1.1f, ModContent.ProjectileType<SecondBuster1>(), Projectile.damage + 30, Projectile.knockBack, Main.myPlayer);
                                busterPlayer.isCharging = false;
                                busterPlayer.stockChargeSecond = true;
                                Projectile.Kill();
                            }
                        }
                        break;
                    }
            }
        }
            
        public override bool PreDraw(ref Color lightColor)
        {
            if(charge < 20)
            {
                return false;
            }
            else { 
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (Projectile.spriteDirection == -1)
                    spriteEffects = SpriteEffects.FlipHorizontally;
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
                int frameHeight = texture.Height / Main.projFrames[Projectile.type];
                int frameWidth = texture.Width / 3;
                int startY = frameHeight * Projectile.frame;
                int startX = fullCharge ? frameWidth * 2 : charge > 100? frameWidth * 1 :0;
                Rectangle sourceRectangle = new Rectangle(startX, startY, texture.Width/3, frameHeight);
                Vector2 origin = sourceRectangle.Size() / 2f;
                float offsetX = 1f;
                origin.X =  (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);
                Color drawColor = Projectile.GetAlpha(lightColor);
                Main.EntitySpriteDraw(texture,
                    Projectile.Center - Main.screenPosition + new Vector2(texture.Width/6, Projectile.gfxOffY),
                    sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
                return false;
            }
        }
    }
}
