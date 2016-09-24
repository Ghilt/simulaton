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


            Needs needs = new Needs();
            needs.Add(Need.ID_HEALTH, new Need(Need.ID_HEALTH, startingHealth, 0, new TerminateEffect(owner, 0f, 0.001f)));
            Need hunger = new Need(Need.ID_NURISHMENT, startingHunger, startingMetabolism, new ModifyNeedEffect(needs[Need.ID_HEALTH], startingHungerDamageThreshold, startingHungerImpact, startingHungerImportance));
            needs.Add(Need.ID_NURISHMENT, hunger);
            return needs;
        }
    }
}
