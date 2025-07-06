using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures; // Necesario para animaciones
using System.Collections.Generic;

namespace moreHealing.Content.Items.Accessories
{
    public class AuricRock : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registra la animación: 16 frames, cambia cada 5 ticks (~12 FPS)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 16));

            // Hace que el ítem se anime cuando está tirado en el mundo
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20; // Coincide con el tamaño de un frame en el sprite
            Item.height = 20;
            Item.accessory = true;
            Item.material = true; // Se puede usar en recetas
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Aumenta el daño general en 5%
            player.GetDamage(DamageClass.Generic) += 0.05f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "AuricRock", "Aumenta el daño en un 5%\nTambién se puede usar como material."));
        }
    }
}