using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace MaverickHunterClass.Content.Projectiles.Weapons
{
    internal class TutorialWandProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 52;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;

            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if(Projectile.ai[0] < 60f)
            {
                Projectile.velocity *= 1.01f;
            } else
            {
                Projectile.velocity *= 1.05f;
                if(Projectile.ai[0] >= 180)
                {
                    Projectile.Kill();
                }
            }

            float rotateSpeed = 0.35f * (float)Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Lighting.AddLight(Projectile.Center, 0.75f, 0.75f, 0.75f);

           
        }
    }
}
