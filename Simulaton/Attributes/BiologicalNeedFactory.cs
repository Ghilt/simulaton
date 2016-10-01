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
            //Health
            float startingHealth = (float)randomizer.NextDouble();

            //Hunger
            float startingHunger = 0.4f + 0.3f * (float)randomizer.NextDouble();
            float startingRate = -0.09f * (float)randomizer.NextDouble();
            float hungerThreshold = 0.3f * (float)randomizer.NextDouble();
            float hungerImpact1 = -0.03f * (float)randomizer.NextDouble();
            float hungerImpact2 = 0.1f * (float)randomizer.NextDouble();
            float hungerImportance = 0.8f + 0.2f * (float)randomizer.NextDouble();

            //Energy
            float startingEnergy = 0.8f + 0.2f * (float)randomizer.NextDouble();
            float energyRate = -0.08f - 0.2f * (float)randomizer.NextDouble();
            float energyThreshold = 0.2f * (float)randomizer.NextDouble();
            float energyImpact = 0.02f * (float)randomizer.NextDouble();
            float energyImportance = 0.8f + 0.2f * (float)randomizer.NextDouble();

            Need health = new Need(Need.ID_HEALTH, startingHealth, 0);
            TerminateEffect terminate = new TerminateEffect(health, owner, 0f, 0.001f);
            health.AddEffect(terminate);

            Need energy = new Need(Need.ID_ENERGY, startingEnergy, energyRate);
            ModifyNeedEffect decHealthMod = new ModifyNeedEffect(energy, health, -energyImpact, energyImportance, energyThreshold, ((x, threshold) => x < threshold));
            energy.AddEffect(decHealthMod);

            Need hunger = new Need(Need.ID_NOURISHMENT, startingHunger, startingRate);
            ModifyNeedEffect dmgHealthMod = new ModifyNeedEffect(hunger, health, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyNeedEffect dmgEnergyMod = new ModifyNeedEffect(hunger, energy, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyNeedEffect healthyMod = new ModifyNeedEffect(hunger, health, hungerImpact2, hungerImportance, 1 - hungerThreshold, ((x, threshold) => x > threshold));
            hunger.AddEffect(dmgHealthMod);
            hunger.AddEffect(dmgEnergyMod);
            hunger.AddEffect(healthyMod);

            Needs needs = new Needs();
            needs.Add(Need.ID_HEALTH, health);
            needs.Add(Need.ID_NOURISHMENT, hunger);
            needs.Add(Need.ID_ENERGY, energy);
            return needs;
        }
    }
}
