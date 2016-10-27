using Simulaton.Attributes;
using Simulaton.Events;
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

        public const int ID_HEALTH = 0;
        public const int ID_NOURISHMENT = 1;
        public const int ID_ENERGY = 2;
        public const int ID_SOCIAL_INTERACTION = 3;

        public const int ID_SEARCH = 0;
        public const int ID_SLEEP = 1;
        public const int ID_SOCIALIZE = 2;

        private static GenerateRandom rand = new GenerateRandom();

        public void SetupTestEnvironment()
        {
            Property.AddToEnvironment(ID_HEALTH, "Health");
            Property.AddToEnvironment(ID_NOURISHMENT, "Food");
            Property.AddToEnvironment(ID_ENERGY, "Energy");
            Property.AddToEnvironment(ID_SOCIAL_INTERACTION, "Social");

            Ability.AddToEnvironment(ID_SEARCH, "Search");
            Ability.AddToEnvironment(ID_SLEEP, "Sleep");
            Ability.AddToEnvironment(ID_SOCIALIZE, "Socialize");
        }

        internal Life CreateHuman(string name, Region region)
        {
            Life human = new Life(0, name, new Location(region, 50, 50));

            AddProperties(human);
            AddAbilities(human);

            return human;
        }

        private void AddAbilities(Life human)
        {
            //Effects from actions
            Interval searchPower = new Interval(0.1f, 0.3f, -1);
            Interval gettingTiredBy = new Interval(-0.05f, -0.1f);
            Interval socializePower = new Interval(0.1f, 0.2f);
            Interval sleepPower = new Interval(0.0f, 0.4f);
            Interval goodOrBadEnergy = new Interval(-0.1f, 0.1f);
            SatisfyEvent finding = new SatisfyFromResourceEvent(searchPower, human.GetLocation());
            SatisfyEvent tieringFromWork = new SatisfyEvent(ID_ENERGY, gettingTiredBy);
            SatisfyEvent energyBoostOrSink = new SatisfyEvent(ID_ENERGY, goodOrBadEnergy);
            SatisfyEvent asleep = new SatisfyEvent(ID_ENERGY, sleepPower);
            SatisfyEvent socializing = new SatisfyEvent(ID_SOCIAL_INTERACTION, socializePower);


            Ability search = new Ability(ID_SEARCH, human);
            search.AddSatisfiableProperty(ID_NOURISHMENT);
            search.AddConsequence(finding);
            search.AddConsequence(tieringFromWork);
            search.AddRequirement(new PropertyRequirement(human, ID_ENERGY, 0.2f, ((x, threshold) => x > threshold)));

            Ability sleep = new Ability(ID_SLEEP, human);
            sleep.AddSatisfiableProperty(ID_ENERGY);
            sleep.AddConsequence(asleep);

            InteractionAbility socialize = new InteractionAbility(ID_SOCIALIZE, human);
            socialize.AddSatisfiableProperty(ID_SOCIAL_INTERACTION);
            socialize.AddConsequence(socializing);
            socialize.AddConsequence(energyBoostOrSink);

            human.AddAbility(search);
            human.AddAbility(sleep);
            human.AddAbility(socialize);
        }

        private void AddProperties(Life human)
        {
            //Health
            float startingHealth = (float)rand.NextDouble();

            //Hunger
            float startingHunger = rand.FloatNear(0.55f);
            float startingRate = rand.FloatNear(-0.04f, 0.01f);
            float hungerThreshold = rand.FloatNear(0.25f);
            float hungerImpact1 = rand.FloatNear(-0.03f);
            float hungerImpact2 = rand.FloatNear(0.1f);
            float hungerImportance = rand.FloatNear(0.9f);

            //Energy
            float startingEnergy = rand.FloatNear(0.8f);
            float energyRate = rand.FloatNear(-0.05f, 0.01f);
            float energyThreshold = rand.FloatNear(0.17f);
            float energyImpact = rand.FloatNear(0.08f);
            float energyImportance = rand.FloatNear(0.8f);

            //Social Interaction
            float startingSocial = rand.FloatNear(0.8f);
            float socialRate = rand.FloatNear(-0.05f, 0.01f);
            float socialThreshold = rand.FloatNear(0.5f);
            float socialImpact = rand.FloatNear(0.03f);
            float socialImportance = rand.FloatNear(0.7f);

            Property health = new Property(ID_HEALTH, startingHealth, 0);
            TerminateEvent terminate = new TerminateEvent(health, human, 0f, 0.001f);
            health.AddEffect(terminate);

            Property energy = new Property(ID_ENERGY, startingEnergy, energyRate);
            ModifyPropertyEvent decHealthMod = new ModifyPropertyEvent(energy, health, -energyImpact, energyImportance, energyThreshold, ((x, threshold) => x < threshold));
            energy.AddEffect(decHealthMod);

            Property hunger = new Property(ID_NOURISHMENT, startingHunger, startingRate);
            ModifyPropertyEvent dmgHealthMod = new ModifyPropertyEvent(hunger, health, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyPropertyEvent dmgEnergyMod = new ModifyPropertyEvent(hunger, energy, hungerImpact1, hungerImportance, hungerThreshold, ((x, threshold) => x < threshold));
            ModifyPropertyEvent healthyMod = new ModifyPropertyEvent(hunger, health, hungerImpact2, hungerImportance, 1 - hungerThreshold, ((x, threshold) => x > threshold));
            hunger.AddEffect(dmgHealthMod);
            hunger.AddEffect(dmgEnergyMod);
            hunger.AddEffect(healthyMod);

            Property social = new Property(ID_SOCIAL_INTERACTION, startingSocial, socialRate);
            ModifyPropertyEvent drainEnergyMod = new ModifyPropertyEvent(social, energy, socialImpact, socialImportance, socialThreshold, ((x, threshold) => x < threshold));
            ModifyPropertyEvent energeticMod = new ModifyPropertyEvent(social, energy, socialImpact, socialImportance, socialThreshold, ((x, threshold) => x > threshold));
            social.AddEffect(drainEnergyMod);
            social.AddEffect(energeticMod);

            human.AddProperty(health);
            human.AddProperty(hunger);
            human.AddProperty(energy);
            human.AddProperty(social);
        }
    }

    class GenerateRandom : Random
    {

        public float FloatNear(float midPoint)
        {
            return FloatNear(midPoint, 0.1f);
        }

        public float FloatNear(float midPoint, float range)
        {
            float ret = (float)((midPoint - range / 2) + range * NextDouble());
            if ((midPoint < 0) != (ret < 0)) return 0; // dont let it swap sign
            return Math.Abs(ret) > 1.0f ? (ret > 0 ? 1.0f : -1.0f) : ret; // if out of bounds(1.0f/-1.0f) return bound
        }
    }
}
