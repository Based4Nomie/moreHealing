using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace moreHealing.Content.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
public class EndlessWings : ModItem
{
    public static int WingSlotID { get; private set; }

    public override string Texture => "moreHealing/Content/Items/Accessories/EndlessWings";

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;

        WingSlotID = Item.wingSlot;
aads
        ArmorIDs.Wing.Sets.Stats[WingSlotID] = new WingStats(int.MaxValue, 8f, 4f);
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 30;
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 10);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.noFallDmg = true;
    }    


    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 1.2f;
        ascentWhenRising = 0.18f;
        maxCanAscendMultiplier = 1.6f;
        maxAscentMultiplier = 3.5f;
        constantAscend = 0.3f;
    }
}
