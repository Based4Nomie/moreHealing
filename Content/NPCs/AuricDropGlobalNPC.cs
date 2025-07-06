using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using moreHealing.Content.Items.Accessories;
using Terraria.GameContent.ItemDropRules;

namespace moreHealing.Content.NPCs
{
    public class AuricDropGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Verifica si el NPC es el Ojo de Cthulhu
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.AuricRock>(), 1, 1, 1));
                // Drop garantizado (1 en 1), 1 unidad
            }
        }
    }
}
