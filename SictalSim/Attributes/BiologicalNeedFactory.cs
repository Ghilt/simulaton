using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class BiologicalNeedFactory : NeedFactory
    {

        public Needs CreateNeeds(Simulator owner)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            float startingHealth = (float)r.NextDouble();
            float startingHunger = 0.6f * (float)r.NextDouble();
            float startingMetabolism = 0.09f * (float)r.NextDouble();
            float startingHungerDamageThreshold = 0.4f * (float)r.NextDouble();
            float startingHungerImpact = -0.04f * (float)r.NextDouble();


            Needs needs = new Needs();
            needs.Add(Need.ID_HEALTH, new Need(Need.ID_HEALTH, startingHealth, 0, new TerminateTriggerEffect(owner, 0f, 0.001f)));
            Need hunger = new Need(Need.ID_HUNGER, startingHunger, startingMetabolism, new ModifyNeedEffect(needs[Need.ID_HEALTH], startingHungerDamageThreshold, startingHungerImpact));
            needs.Add(Need.ID_HUNGER, hunger);
            return needs;
        }
    }
}
