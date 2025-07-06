using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace moreHealing.Content.Items
{
    public class GigaHealingPotion : ModItem
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
            Item.healLife = 500;
            Item.potion = true;
        }

        public override void AddRecipes()
        {
            if (!ModLoader.HasMod("CalamityMod"))
                return;

            Mod calamity = ModLoader.GetMod("CalamityMod");

            int omegaPotion     = calamity.Find<ModItem>("OmegaHealingPotion").Type;
            int bloodOrange     = calamity.Find<ModItem>("BloodOrange").Type;
            int miracleFruit    = calamity.Find<ModItem>("MiracleFruit").Type;
            int elderberry      = calamity.Find<ModItem>("Elderberry").Type;
            int dragonfruit     = calamity.Find<ModItem>("Dragonfruit").Type;
            int draedonsForge   = calamity.Find<ModTile>("DraedonsForge").Type;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(omegaPotion);
            recipe.AddIngredient(bloodOrange);
            recipe.AddIngredient(miracleFruit);
            recipe.AddIngredient(elderberry);
            recipe.AddIngredient(dragonfruit);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.LifeFruit);
            recipe.AddTile(draedonsForge); // ← Requiere la forja de Draedon
            recipe.Register();
        }
    }
}

