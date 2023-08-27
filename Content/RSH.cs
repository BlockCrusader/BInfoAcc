using BInfoAcc.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Content
{
	public class RSH : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().comboDisplay = true;
			player.GetModPlayer<ComboPlayer>().trackCombos = true;

			player.GetModPlayer<InfoPlayer>().regenDisplay = true;
			player.GetModPlayer<InfoPlayer>().spawnRateDisplay = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SafteyScanner>()
				.AddIngredient<HitMarker>()
				.AddIngredient<SmartHeart>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}