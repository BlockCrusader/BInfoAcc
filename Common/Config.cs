using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ArtificerMod.Common
{
	public class ConfigServer : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[DefaultValue(false)]
		[ReloadRequired]
		public bool easySell;

		[DefaultValue(true)]
		[ReloadRequired]
		public bool extendedRecipe;

		[DefaultValue(true)]
		[ReloadRequired]
		public bool updatedPhones;
	}
}
