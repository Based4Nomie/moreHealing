using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalamityMod.Items.Weapons.Melee
{
    public class EdgedExoblade : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";

        public static readonly SoundStyle SwingSound = new("CalamityMod/Sounds/Item/ExobladeSwing") { MaxInstances = 3, PitchVariance = 0.6f, Volume = 0.8f };

        public override void SetDefaults()
        {
            Item.width = 138;
            Item.height = 184;
            Item.damage = 12000; // Daño personalizado
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 49;
            Item.useAnimation = 49;
            Item.useTurn = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 9f;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
            Item.shoot = ProjectileType<ExobladeProj>(); // Usa el mismo proj que la Exoblade
            Item.shootSpeed = 9f;
            Item.rare = RarityType<Violet>();
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool? CanHitNPC(Player player, NPC target) => false;
        public override bool CanHitPvp(Player player, Player target) => false;

        public override bool CanShoot(Player player)
        {
            // Solo evita disparar si ya hay una espada activa (evita spam)
            return !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == Item.shoot);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Dispara 3 beams frontales al usar clic derecho
                float spread = MathHelper.ToRadians(15f);
                for (int i = -1; i <= 1; i++)
                {
                    Vector2 newVelocity = velocity.RotatedBy(i * spread) * 1.2f;
                    Projectile.NewProjectile(source, position, newVelocity, ProjectileType<Exobeam>(), damage, knockback, player.whoAmI);
                }
                return false;
            }

            // Ataque normal (melee con beam)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0);
            return false;
        }

        public override void HoldItem(Player player)
        {
            player.Calamity().rightClickListener = true;
            player.Calamity().mouseWorldListener = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ExobladeGlow").Value);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Exoblade>()
                .AddIngredient<MiracleMatter>(2)
                .AddTile(TileType<DraedonsForge>())
                .Register();
        }
    }
}
