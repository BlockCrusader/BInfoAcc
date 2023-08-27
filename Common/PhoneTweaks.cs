using ArtificerMod.Common;
using BInfoAcc.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
    public class RecipeTweak : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModContent.GetInstance<ConfigServer>().extendedRecipe)
            {
                return;
            }
            // Disables PDA recipes
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasIngredient(ItemID.GPS) && recipe.HasIngredient(ItemID.FishFinder) 
                    && recipe.HasIngredient(ItemID.GoblinTech) && recipe.HasIngredient(ItemID.REK)
                    && (!recipe.HasIngredient<ScryingMirror>() || !recipe.HasIngredient<RSH>())
                    && recipe.HasTile(TileID.TinkerersWorkbench) 
                    && recipe.HasResult(ItemID.PDA))
                {
                    recipe.DisableRecipe();
                }
            }
        }

        public override void AddRecipes()
        {
            if (!ModContent.GetInstance<ConfigServer>().extendedRecipe)
            {
                return;
            }

            // Re-ads PDA recipe with new info accessories
            Recipe pda = Recipe.Create(ItemID.PDA);
            pda.AddIngredient(ItemID.GPS);
            pda.AddIngredient(ItemID.FishFinder);
            pda.AddIngredient(ItemID.GoblinTech);
            pda.AddIngredient(ItemID.REK);
            pda.AddIngredient<ScryingMirror>();
            pda.AddIngredient<RSH>();
            pda.AddTile(TileID.TinkerersWorkbench);
            pda.Register();
        }
    }

    public class PhoneTweaks : GlobalItem 
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.PDA || entity.type == ItemID.CellPhone ||
                entity.type == ItemID.Shellphone || entity.type == ItemID.ShellphoneSpawn
                || entity.type == ItemID.ShellphoneHell || entity.type == ItemID.ShellphoneOcean;
        }

        public override void UpdateInfoAccessory(Item item, Player player)
        {
            if (!ModContent.GetInstance<ConfigServer>().updatedPhones)
            {
                return;
            }

            player.GetModPlayer<InfoPlayer>().comboDisplay = true;
            player.GetModPlayer<ComboPlayer>().trackCombos = true;
            player.GetModPlayer<InfoPlayer>().regenDisplay = true;
            player.GetModPlayer<InfoPlayer>().spawnRateDisplay = true;
            player.GetModPlayer<InfoPlayer>().minionDisplay = true;
            player.GetModPlayer<InfoPlayer>().sentryDisplay = true;
            player.GetModPlayer<InfoPlayer>().luckDisplay = true;
        }
    }
}