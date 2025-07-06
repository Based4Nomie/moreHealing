using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace moreHealing.Content.Items
{
    public class DeltaHealingPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 50);
            Item.healLife = 750;
            Item.potion = true;
        }

        public override void AddRecipes()
        {
            if (!ModLoader.HasMod("CalamityMod"))
                return;

            Mod calamity = ModLoader.GetMod("CalamityMod");

            int miracleMatter   = calamity.Find<ModItem>("MiracleMatter").Type;
            int draedonsForge   = calamity.Find<ModTile>("DraedonsForge").Type;

            int gigaPotion = ModContent.ItemType<GigaHealingPotion>();

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(gigaPotion);      // De TU mod
            recipe.AddIngredient(miracleMatter);   // De Calamity
            recipe.AddTile(draedonsForge);         // Forja de Draedon
            recipe.Register();
        }
    }
}
