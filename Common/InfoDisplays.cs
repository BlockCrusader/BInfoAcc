using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Humanizer;

namespace BInfoAcc.Common
{
	public class LuckInfoDisplay : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().luckDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			float luckValue = Main.LocalPlayer.luck;
			luckValue = (float)Math.Round(luckValue, 2);

			if (luckValue == 0)
			{
				displayColor = InactiveInfoTextColor;
			}
			else if (luckValue > 0)
            {
				displayColor = new Color(120, 190, 120);
			}
			else if (luckValue < 0)
            {
				displayColor = new Color(190, 120, 120);
			}

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.LuckDisplay").FormatWith(luckValue);
        }
	}

	public class MinionInfoDisplay : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().minionDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			float minionCount = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.minionSlots > 0f && proj.owner == Main.myPlayer)
				{
					minionCount += proj.minionSlots;
				}
			}

			minionCount = (float)Math.Round(minionCount, 2);

			int maxMinions = Main.LocalPlayer.maxMinions;

			if (minionCount == 0)
			{
				displayColor = InactiveInfoTextColor;
			}

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.MinionDisplay").FormatWith(minionCount, maxMinions);
        }
	}

	public class SentryInfoDisplay : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().sentryDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			int sentryCount = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.sentry && proj.owner == Main.myPlayer)
				{
					sentryCount ++;
				}
			}

			int maxSentries = Main.LocalPlayer.maxTurrets;

			if (sentryCount == 0)
			{
				displayColor = InactiveInfoTextColor;
			}

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SentryDisplay").FormatWith(sentryCount, maxSentries);
		}
	}

	public class RegenInfoDisplay : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().regenDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			float lifeRegen = Main.LocalPlayer.lifeRegen;
			lifeRegen *= 0.5f;
			lifeRegen = (float)Math.Round(lifeRegen, 2);

			if (lifeRegen == 0)
			{
				displayColor = InactiveInfoTextColor;
			}
			else if (lifeRegen > 0)
			{
				if(Main.LocalPlayer.statLife >= Main.LocalPlayer.statLifeMax2)
                {
					return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayFull");
                }
				displayColor = new Color(120, 190, 120);
			}
			else if (lifeRegen < 0)
			{
				displayColor = new Color(190, 120, 120);
			}

			return lifeRegen > 0 ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayPos").FormatWith(lifeRegen) : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayNeg").FormatWith(lifeRegen);
        }
	}

	public class SpawnRateInfoDisplay : InfoDisplay
	{
		

		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().spawnRateDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			int spawnRateRaw = Main.LocalPlayer.GetModPlayer<InfoPlayer>().spawnRate;

			if(spawnRateRaw == 0)
            {
				return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnRateDisplayError");
            }

			// Spawn Rate is the chance for an enemy spawn per tick; this translates that chance to be per second instead
			float spawnRateAdapted =  60f / spawnRateRaw;

			spawnRateAdapted = (float)Math.Round(spawnRateAdapted, 2);

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnRateDisplay").FormatWith(spawnRateAdapted);
        }
	}

	public class ComboInfoDisplay : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().comboDisplay;
		}

		public override string DisplayValue(ref Color displayColor)
		{
			ComboPlayer player = Main.LocalPlayer.GetModPlayer<ComboPlayer>();

			int hits = player.hitCounter;
			int dmg = player.comboDmg;
			float time = player.comboTime / 60f;
			time = (float)Math.Round(time, 1);

			if (hits == 0)
			{
				displayColor = InactiveInfoTextColor;
				return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayNone");
            }

			// Convert to strings
			string hitCount = CompactNumbers(hits);
			string timeCount = CompactTime(time);
			string dmgCount = CompactNumbers(dmg);

			return player.displayToggle > 250 ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayTime").FormatWith(hitCount ,timeCount) 
											  : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayDmg").FormatWith(hitCount, dmgCount);
        }

		private static string CompactNumbers(int baseInt)
        {
			float baseFloat = (float)baseInt;
			string suffix = "";
			if(baseFloat < 0)
            {
				return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboError");
            }

			if (baseFloat >= 100000000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixBillion");
                baseFloat /= 1000000000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			else if (baseFloat >= 100000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixMillion");
                baseFloat /= 1000000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			else if (baseFloat >= 1000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixThousand");
                baseFloat /= 1000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			return $"{baseFloat}" + suffix;
		}

		private string CompactTime(float baseNum)
        {
			string suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixSeconds");
            if (baseNum >= 3600f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixHours");
                baseNum /= 3600f;
				baseNum = (float)Math.Round(baseNum, 1);
			}
			else if (baseNum >= 60f)
            {
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixMinutes");
                baseNum /= 60f;
				baseNum = (float)Math.Round(baseNum, 1);
			}
			return $"{baseNum}" + suffix;
		}
	}
}