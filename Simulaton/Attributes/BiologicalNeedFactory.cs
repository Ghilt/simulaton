using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class BiologicalNeedFactory : NeedFactory
    {
        private static Random randomizer = new Random();

        public Needs CreateNeeds(Simulator owner)
        {
            float startingHealth = (float)randomizer.NextDouble();
            float startingHunger = 0.5f + 0.4f * (float)randomizer.NextDouble();
            float startingMetabolism = -0.09f * (float)randomizer.NextDouble();
            float startingHungerDamageThreshold = 0.4f * (float)randomizer.NextDouble();
            float startingHungerImpact = -0.04f * (float)randomizer.NextDouble();
            float startingHungerImportance = 1.0f + 0.0f * (float)randomizer.NextDouble(); // max


            Need health = new Need(Need.ID_HEALTH, startingHealth, 0);
            TerminateEffect terminate = new TerminateEffect(health, owner, 0f, 0.001f);
            health.AddEffect(terminate);

            Need hunger = new Need(Need.ID_NOURISHMENT, startingHunger, startingMetabolism);
            ModifyNeedEffect hungerMod = new ModifyNeedEffect(hunger, health, startingHungerDamageThreshold, startingHungerImpact, startingHungerImportance);
            hunger.AddEffect(hungerMod);

            Needs needs = new Needs();
            needs.Add(Need.ID_HEALTH, health);
            needs.Add(Need.ID_NOURISHMENT, hunger);
            return needs;
        }
    }
}
