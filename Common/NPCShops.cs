﻿using BInfoAcc.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ArtificerMod.Common
{
	class ArsenalShop : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if(shop.NpcType == NPCID.GoblinTinkerer)
            {
				shop.Add<EngiRegistry>();
            }
			if (shop.NpcType == NPCID.SkeletonMerchant)
			{
                var safteyScannerCondition = new Condition("Mods.BInfoAcc.CommonItemtooltip.ScannerCondition", () => 
				Condition.EclipseOrBloodMoon.IsMet() || 
				((Condition.InRockLayerHeight.IsMet() || Condition.InDirtLayerHeight.IsMet() || Condition.InUnderworldHeight.IsMet()) && 
				(Condition.InUndergroundDesert.IsMet() || Condition.InSnow.IsMet() || Condition.InJungle.IsMet() || 
				Condition.InHallow.IsMet() || Condition.InCorrupt.IsMet() || Condition.InCrimson.IsMet()))
				);

				shop.Add<SafteyScanner>(safteyScannerCondition);
			}

			if (shop.NpcType == NPCID.Merchant)
			{
				var merchantSalesCondition = new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantSaleCondition", () => ModContent.GetInstance<ConfigServer>().easySell);

				shop.Add<SmartHeart>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionWnG", () => 
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningGibbous.IsMet()));
				shop.Add<AttendanceLog>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionTQ", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseThirdQuarter.IsMet()));
				shop.Add<SafteyScanner>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionWnC", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningCrescent.IsMet()));
				shop.Add<HitMarker>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionWxG", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaxingCrescent.IsMet()));
				shop.Add<EngiRegistry>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionFQ", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseFirstQuarter.IsMet()));
				shop.Add<FortuneMirror>(new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionWxC", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaxingGibbous.IsMet()));
			}
		}
    }
}
