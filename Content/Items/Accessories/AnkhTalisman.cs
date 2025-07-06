using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreHealing.Content.Items.Accessories
{
    public class AnkhTalisman : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MoreHealing.Common.Players.AnkhPlayer>().hasAnkhTalisman = true;

            player.lifeRegen += 25;

            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.noKnockback = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip",
                "Un símbolo de curación y resistencia sagrada.\n" +
                "Reduce la duración del debuff de pociones.\n" +
                "Otorga inmunidad a la mayoría de los debuffs.\n" +
                "Mejora la regeneración de vida."));
        }

        public override void AddRecipes()
        {
            if (!ModLoader.HasMod("CalamityMod"))
                return;

            Mod calamity = ModLoader.GetMod("CalamityMod");

            int draedonsForge = calamity.Find<ModTile>("DraedonsForge").Type;
            int auricBar = calamity.Find<ModItem>("AuricBar").Type;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddIngredient(auricBar, 5);
            recipe.AddTile(draedonsForge);
            recipe.Register();
        }
    }
}

