using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.ModLoader;

namespace moreHealing.Content.Projectiles
{
    public class HollowPulseCore : ModProjectile
    {
        private int explosionTimer = 90; // 1.5 segundos antes de explotar
        private bool hasExploded = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 13; // <- Número de frames
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.6f, 0f, 0.9f);

            // Animación de los frames
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5) // velocidad de animación
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }

            // Efectos visuales
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, Scale: 1.5f);

            // Atracción de enemigos cercanos
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(this) && Vector2.Distance(npc.Center, Projectile.Center) < 300f)
                {
                    Vector2 pull = Projectile.Center - npc.Center;
                    pull.Normalize();
                    pull *= 2f;
                    npc.velocity += pull * 0.1f;
                }
            }

            explosionTimer--;
            if (explosionTimer <= 0 && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }

        private void Explode()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.position -= new Vector2(105, 105); // Centrar explosión
            Projectile.damage *= 4;
            Projectile.knockBack *= 3;

            // Efecto de explosión
            for (int i = 0; i < 60; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 2f);
            }

            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            Projectile.Kill(); // Destruir el proyectil tras la explosión
        }
    }
}
