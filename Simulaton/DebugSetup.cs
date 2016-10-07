using Simulaton.Attributes;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class DebugSetup
    {
        private static Random randomizer = new Random();

        internal static Life CreateHuman()
        {
            Region r = new Region(0, 100, 100);
            Life human = new Life(0, new Location(r, 50, 50));

            AddProperties(human);
            AddAbilities(human);

            return human;
        }

        private static void AddAbilities(Life human)
        {
            Ability search = new Ability(Ability.ID_SEARCH, human);
            search.AddsatisfiableNeed(Property.ID_NOURISHMENT);
            Interval searchPower = new Interval(0.0f, 0.4f, -1);
            SatisfyConsequence finding = new SatisfyConsequence(searchPower, human.GetLocation());
            search.AddConsequence(finding);

            Ability sleep = new Ability(Ability.ID_SLEEP, human);
            sleep.AddsatisfiableNeed(Property.ID_ENERGY);
            Interval sleepPower = new Interval(0.0f, 0.2f);
            SatisfyConsequence asleep = new SatisfyConsequence(Property.ID_ENERGY, sleepPower, null);
            sleep.AddConsequence(asleep);

            human.AddAbility(search);
            human.AddAbility(sleep);
        }

        private static void AddProperties(Life human)
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
            TerminateEffect terminate = new TerminateEffect(health, human, 0f, 0.001f);
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

            human.AddProperty(health);
            human.AddProperty(hunger);
            human.AddProperty(energy);
        }
    }
}
