
using log4net;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Diagnostics;

namespace MaverickHunterClass.Common.Players
{
	// This class showcases things you can do with fishing
	public class BusterPlayer : ModPlayer
	{
		public int activeBusterShots;
        public int maxBusterShots = 3;
		public bool isCharging = false;
		public int chargeSpeed = 1;
		public bool ultimateBuster = false;
		public int initialCharge = 0;
		public int spiralShotNum = 0;
		public bool stockChargeSecond = false;
		public int stockChargeThird = 0;
		public bool thirdShotCollide = false;
		public bool thirdShot2Collide = false;
		public Rectangle? thirdShot1Rectangle = null;
        public Rectangle? thirdShot2Rectangle = null;

        public override void ResetEffects()
        {
            ultimateBuster = false;
			chargeSpeed = 1;
            initialCharge = 0;
            maxBusterShots = 3;
        }

		public override void FrameEffects()
		{
			if (stockChargeSecond)
			{
                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.PinkTorch, default, default, default);
            }

			switch (stockChargeThird)
			{
				case 1:
                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.PinkTorch, default, default, default);
                    break;
                case 2:
                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.GoldCoin, default, default, default);
                    break;
            }

			
		}

		public bool thirdShotIntersectLogic()
		{
            if (thirdShot1Rectangle != null && thirdShot2Rectangle != null)
            {
                if (thirdShot1Rectangle.Value.Intersects(thirdShot2Rectangle.Value))
                {
                    thirdShotCollide = true;
					return true;
                }
            }
			thirdShotCollide = false;
            return false;
		}
     }
}
