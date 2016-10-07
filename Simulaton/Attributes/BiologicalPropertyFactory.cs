using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class BiologicalPropertyFactory : PropertyFactory
    {
        private static Random randomizer = new Random();

        public Properties CreateNeeds(Simulator owner)
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

            Property health = new Property(Property.ID_HEALTH, startingHealth, 0);
            TerminateEffect terminate = new TerminateEffect(health, owner, 0f, 0.001f);
            health.AddEffect(terminate);

            Property energy = new Property(Property.ID_ENERGY, startingEnergy, energyRate);
            ModifyPropertyEffect decHealthMod = new ModifyPropertyEffect(energy, health, -energyImpact, energyImportance, energyThreshold, ((x, threshold) => x < threshold));
            energy.AddEffect(decHealthMod);

            Property hunger = new Property(Property.ID_NOURISHMENT, startingHunger, startingRate);
            ModifyPropertyEffect dmgHealthMod = new ModifyPropertyEffect(hunger, health, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyPropertyEffect dmgEnergyMod = new ModifyPropertyEffect(hunger, energy, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyPropertyEffect healthyMod = new ModifyPropertyEffect(hunger, health, hungerImpact2, hungerImportance, 1 - hungerThreshold, ((x, threshold) => x > threshold));
            hunger.AddEffect(dmgHealthMod);
            hunger.AddEffect(dmgEnergyMod);
            hunger.AddEffect(healthyMod);

            Properties needs = new Properties();
            needs.Add(Property.ID_HEALTH, health);
            needs.Add(Property.ID_NOURISHMENT, hunger);
            needs.Add(Property.ID_ENERGY, energy);
            return needs;
        }
    }
}
