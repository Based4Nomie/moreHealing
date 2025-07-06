using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace moreHealing.Content.Items.Accessories
{
    public class AuricHunter : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "AuricHunter",
                "Aumenta tu daño y vida según el jefe más fuerte derrotado:\n" +
                "+50% daño, +100 vida con el Muro Carnoso\n" +
                "+100% daño, +200 vida con Plantera\n" +
                "+175% daño, +350 vida con Moon Lord\n" +
                "+375% daño, +500 vida con Polterghast\n" +
                "+450% daño, +800 vida con el Devorador de Dioses"));
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float damageBonus = 0f;
            int lifeBonus = 0;

            if (HasDefeatedDoG())
            {
                damageBonus = 4.5f;
                lifeBonus = 800;
            }
            else if (HasDefeatedPolterghast())
            {
                damageBonus = 3.75f;
                lifeBonus = 500;
            }
            else if (NPC.downedMoonlord)
            {
                damageBonus = 1.75f;
                lifeBonus = 350;
            }
            else if (NPC.downedPlantBoss)
            {
                damageBonus = 1.0f;
                lifeBonus = 200;
            }
            else if (Main.hardMode)
            {
                damageBonus = 0.5f;
                lifeBonus = 100;
            }

            player.GetDamage(DamageClass.Generic) += damageBonus;
            player.statLifeMax2 += lifeBonus;
        }

        private bool HasDefeatedDoG()
        {
            if (!ModLoader.HasMod("CalamityMod"))
                return false;

            object result = ModLoader.GetMod("CalamityMod")?.Call("Downed", "devourerofgods");
            return result is bool b && b;
        }

        private bool HasDefeatedPolterghast()
        {
            if (!ModLoader.HasMod("CalamityMod"))
                return false;

            object result = ModLoader.GetMod("CalamityMod")?.Call("Downed", "polterghast");
            return result is bool b && b;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 50);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ModContent.ItemType<AuricRock>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

