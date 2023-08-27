using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
	public class InfoPlayer : ModPlayer
	{
		public bool minionDisplay;
		public bool luckDisplay;
		public bool sentryDisplay;
		public bool comboDisplay;
		public bool spawnRateDisplay;
		public bool regenDisplay;

		// Limits how oftend the spawn rate display can update, to prevent flickering numbers
		public int spawnRateUpdateTimer;
		public int spawnRate; // The actual spawn rate recorded
		static FieldInfo spawnRateFieldInfo; // Used in reflection for obtaining spawn rate

		public override void ResetInfoAccessories()
		{
			minionDisplay = false;
			luckDisplay = false;
			sentryDisplay = false;
			comboDisplay = false;
			spawnRateDisplay = false;
			regenDisplay = false;
		}

		public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
		{
			if (otherPlayer.GetModPlayer<InfoPlayer>().minionDisplay)
			{
				minionDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().luckDisplay)
			{
				luckDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().sentryDisplay)
			{
				sentryDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().comboDisplay)
			{
				comboDisplay = true;
				Player.GetModPlayer<ComboPlayer>().trackCombos = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().spawnRateDisplay)
			{
				spawnRateDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().regenDisplay)
			{
				regenDisplay = true;
			}
		}

        public override void PreUpdate()
        {
            if(spawnRateUpdateTimer > 0)
            {
				spawnRateUpdateTimer--;
			}
        }

        public override void PostUpdateMiscEffects()
        {
            if(spawnRateUpdateTimer <= 0)
            {
				spawnRateUpdateTimer = 60;

				// !Reflection!
				spawnRateFieldInfo = typeof(NPC).GetField("spawnRate", BindingFlags.Static | BindingFlags.NonPublic);

				spawnRate = (int)spawnRateFieldInfo.GetValue(null);
			}
        }
    }
}