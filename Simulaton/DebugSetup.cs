using Simulaton.Attributes;
using Simulaton.Events;
using Simulaton.Mechanics;
using Simulaton.Mechanics.ValueTransformFunctions;
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
        public const int ID_PROPERTY_HEALTH = 0;
        public const int ID_PROPERTY_AGE = 1;
        public const int ID_PROPERTY_NOURISHMENT = 2;
        public const int ID_PROPERTY_ENERGY = 3;
        public const int ID_PROPERTY_SOCIAL_INTERACTION = 4;

        public const int ID_ABILITY_SEARCH = 0;
        public const int ID_ABILITY_SLEEP = 1;
        public const int ID_ABILITY_SOCIALIZE = 2;

        private static GenerateRandom rand = new GenerateRandom();

        public void SetupTestEnvironment()
        {
            Property.AddToEnvironment(ID_PROPERTY_HEALTH, "Health");
            Property.AddToEnvironment(ID_PROPERTY_AGE, "Age");
            Property.AddToEnvironment(ID_PROPERTY_NOURISHMENT, "Food");
            Property.AddToEnvironment(ID_PROPERTY_ENERGY, "Energy");
            Property.AddToEnvironment(ID_PROPERTY_SOCIAL_INTERACTION, "Social");

            Ability.AddToEnvironment(ID_ABILITY_SEARCH, "Search");
            Ability.AddToEnvironment(ID_ABILITY_SLEEP, "Sleep");
            Ability.AddToEnvironment(ID_ABILITY_SOCIALIZE, "Socialize");
        }

        internal Life CreateHuman(string name, Region region)
        {
            Life human = new Life(0, name, new Location(region, 50, 50));

            AddProperties(human);
            AddPropertyUpdaters(human);
            AddAbilities(human);

            return human;
        }

        private void AddProperties(Life human)
        {
            //Health
            float startingHealth = rand.FloatNear(0.55f);
            //age
            float startingAge = 25f;
            //Hunger
            float startingHunger = rand.FloatNear(0.55f);
            //Energy
            float startingEnergy = rand.FloatNear(0.8f);
            //Social Interaction
            float startingSocial = rand.FloatNear(0.8f);

            Property health = new Property(ID_PROPERTY_HEALTH, startingHealth);
            Property age = new Property(ID_PROPERTY_AGE, startingAge, false);
            Property hunger = new Property(ID_PROPERTY_NOURISHMENT, startingHunger);
            Property energy = new Property(ID_PROPERTY_ENERGY, startingEnergy);
            Property social = new Property(ID_PROPERTY_SOCIAL_INTERACTION, startingSocial);

            human.AddProperty(health);
            human.AddProperty(age);
            human.AddProperty(hunger);
            human.AddProperty(energy);
            human.AddProperty(social);

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
            SatisfyEvent tieringFromWork = new SatisfyEvent(ID_PROPERTY_ENERGY, gettingTiredBy);
            tieringFromWork.AddModifier(new AbilityModifier(ID_PROPERTY_AGE, (x => x > 60), 2.0f));
            SatisfyEvent energyBoostOrSink = new SatisfyEvent(ID_PROPERTY_ENERGY, goodOrBadEnergy);
            SatisfyEvent asleep = new SatisfyEvent(ID_PROPERTY_ENERGY, sleepPower);
            asleep.AddModifier(new AbilityModifier(ID_PROPERTY_NOURISHMENT, (x => x < 0.4f), 0.2f));
            SatisfyEvent socializing = new SatisfyEvent(ID_PROPERTY_SOCIAL_INTERACTION, socializePower);

            Ability search = new Ability(ID_ABILITY_SEARCH, human);
            search.AddSatisfiableProperty(ID_PROPERTY_NOURISHMENT);
            search.AddConsequence(finding);
            search.AddConsequence(tieringFromWork);
            search.AddRequirement(new RequirePropertyAmount(human, ID_PROPERTY_ENERGY, 0.2f, ((x, threshold) => x > threshold)));

            Ability sleep = new Ability(ID_ABILITY_SLEEP, human);
            sleep.AddSatisfiableProperty(ID_PROPERTY_ENERGY);
            sleep.AddConsequence(asleep);

            InteractionAbility socialize = new InteractionAbility(ID_ABILITY_SOCIALIZE, human);
            socialize.AddSatisfiableProperty(ID_PROPERTY_SOCIAL_INTERACTION);
            socialize.AddConsequence(socializing);
            socialize.AddConsequence(energyBoostOrSink);

            human.AddAbility(search);
            human.AddAbility(sleep);
            human.AddAbility(socialize);
        }

        private void AddPropertyUpdaters(Life human)
        {

            //Hunger
            float startingRate = rand.FloatNear(-0.04f, 0.01f);
            float hungerThreshold = rand.FloatNear(0.25f);
            float hungerImpact1 = rand.FloatNear(-0.03f);
            float hungerImpact2 = rand.FloatNear(0.1f);
            float hungerImportance = rand.FloatNear(0.9f);

            //Age 
            float ageDeath = 100f;

            //Energy
            float energyRate = rand.FloatNear(-0.05f, 0.01f);
            float energyThreshold = rand.FloatNear(0.17f);
            float energyImpact = rand.FloatNear(0.08f);
            float energyImportance = rand.FloatNear(0.8f);

            //Social Interaction
            float socialRate = rand.FloatNear(-0.05f, 0.01f);
            float socialThreshold = rand.FloatNear(0.5f);
            float socialImpact = rand.FloatNear(0.03f);
            float socialImportance = rand.FloatNear(0.7f);

            PropertyUpdater health = new PropertyUpdater(human.GetProperty(ID_PROPERTY_HEALTH), 0);
            TerminateEvent terminate = new TerminateEvent(health, human, (x => x < 0.01));
            health.AddEffect(terminate);

            PropertyUpdater aging = new PropertyUpdater(human.GetProperty(ID_PROPERTY_AGE), 0.34f);
            TerminateEvent timeUp = new TerminateEvent(aging, human, (x => x > ageDeath));
            aging.AddEffect(timeUp);


            PropertyUpdater energy = new PropertyUpdater(human.GetProperty(ID_PROPERTY_ENERGY), energyRate);

            ModifyPropertyEvent decHealthMod = new ModifyPropertyEvent(energy, health, -energyImpact, energyImportance, new SmallerThanThreshold(energyThreshold, -energyImpact));
            energy.AddEffect(decHealthMod);

            PropertyUpdater hunger = new PropertyUpdater(human.GetProperty(ID_PROPERTY_NOURISHMENT), startingRate);
            ModifyPropertyEvent dmgHealthMod = new ModifyPropertyEvent(hunger, health, hungerImpact1, hungerImportance, new SmallerThanThreshold(hungerThreshold, hungerImpact1));
            ModifyPropertyEvent dmgEnergyMod = new ModifyPropertyEvent(hunger, energy, hungerImpact1, hungerImportance, new SmallerThanThreshold(hungerThreshold, hungerImpact1));
            ModifyPropertyEvent healthyMod = new ModifyPropertyEvent(hunger, health, hungerImpact2, hungerImportance, new LargerThanThreshold(1 - hungerThreshold, hungerImpact2));
            hunger.AddEffect(dmgHealthMod);
            hunger.AddEffect(dmgEnergyMod);
            hunger.AddEffect(healthyMod);

            PropertyUpdater social = new PropertyUpdater(human.GetProperty(ID_PROPERTY_SOCIAL_INTERACTION), socialRate);
            ModifyPropertyEvent drainEnergyMod = new ModifyPropertyEvent(social, energy, socialImpact, socialImportance, new SmallerThanThreshold(socialThreshold, socialImpact));
            ModifyPropertyEvent energeticMod = new ModifyPropertyEvent(social, energy, socialImpact, socialImportance, new LargerThanThreshold(socialThreshold, socialImpact));
            social.AddEffect(drainEnergyMod);
            social.AddEffect(energeticMod);

            human.AddPropertyUpdater(health);
            human.AddPropertyUpdater(aging);
            human.AddPropertyUpdater(hunger);
            human.AddPropertyUpdater(energy);
            human.AddPropertyUpdater(social);
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
