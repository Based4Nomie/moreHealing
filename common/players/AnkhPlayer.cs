using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreHealing.Common.Players
{
    public class AnkhPlayer : ModPlayer
    {
        public bool hasAnkhTalisman;

        public override void ResetEffects()
        {
            hasAnkhTalisman = false;
        }

        public override void PostUpdate()
        {
            if (hasAnkhTalisman)
            {
                for (int i = 0; i < Player.buffType.Length; i++)
                {
                    if (Player.buffType[i] == BuffID.PotionSickness && Player.buffTime[i] > 1500)
                    {
                        Player.buffTime[i] = 1500;
                    }
                }
            }
        }
    }
}
