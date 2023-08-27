using Terraria;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
	public class ComboPlayer : ModPlayer
	{
		public int hitCounter;
		public int comboTime;
		public int comboDmg;
		int expirationCounter = -1;
		public bool trackCombos;
		public int displayToggle;
		public override void PreUpdate()
		{
			if (hitCounter > 0 && expirationCounter > 0 && !Player.dead && Player.active)
			{
				expirationCounter--;
				comboTime++;
			}
			else
			{
				hitCounter = 0;
				comboTime = 0;
				comboDmg = 0;
				expirationCounter = -1;
			}
			displayToggle++;
			if(displayToggle > 500)
            {
				displayToggle = 1;
            }
		}


		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
		{
			IncrementCounter(damageDone);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
			IncrementCounter(damageDone);
		}

		private void IncrementCounter(int dmg)
		{
			if (Player.dead || !Player.active)
			{
				return;
			}
			hitCounter++;
			comboDmg += dmg;

			// Hard cap values to avoid integer limit
			if (hitCounter >= int.MaxValue - 1) 
			{
				hitCounter = int.MaxValue - 1;
			}
			if (comboDmg >= int.MaxValue - 1)
			{
				comboDmg = int.MaxValue - 1;
			}
			expirationCounter = 180;
		}
	}
}