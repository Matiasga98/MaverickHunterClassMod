
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
		public int spiralShotNum = 0;

        public override void ResetEffects()
        {
            ultimateBuster = false;
			chargeSpeed = 1;
			maxBusterShots = 3;
        }

		public void spiralShotAdd()
		{
			spiralShotNum++;
			if (spiralShotNum == 3)
				spiralShotNum = 0;
		}
    }
}
