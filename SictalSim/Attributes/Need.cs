using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class Need
    {
        public const int ID_HEALTH = 0;
        public const int ID_HUNGER = 1;

        public int id { get; private set; }
        public float amount { get; private set; } // between 0 and 1
        private float rate;
        private List<Effect> effects;
        private TerminateTriggerEffect terminateTriggerEffect;

        public Need(int id, float amount)
        {
            this.id = id;
            this.amount = amount;
            this.rate = 0;
            this.effects = new List<Effect>();
        }

        public Need(int id, float amount, float rate) : this(id, amount)
        {
            this.rate = rate;
        }

        public Need(int id, float amount, float rate, List<Effect> effects) : this(id, amount, rate)
        {
            this.effects = effects;
        }

        public Need(int id, float amount, float rate, Effect effect) : this(id, amount, rate)
        {
            this.effects = new List<Effect>();
            this.effects.Add(effect);
        }

        public void Tick()
        {
            float relevantLimit = rate > 0 ? 1 : 0;
            float newValue = amount + rate;
            bool isOutOfLimit = relevantLimit == 1 ? newValue < relevantLimit : newValue > relevantLimit;
            amount = isOutOfLimit ? newValue : relevantLimit;
        }

        internal void Affect()
        {
            foreach (Effect effect in effects)
            {
                effect.OnTrigger(this);
            }
        }

        public void Modify(float quantity)
        {
            amount -= amount < quantity ? amount : quantity;
        }

        public override string ToString()
        {
            return ((int)(amount * 100) + "%");
        }

        internal void addEffect(Effect effect)
        {
            effects.Add(effect);
        }
    }

    class NeedFactory
    {

        internal static Dictionary<int, Need> CreateBasicBiologicalNeeds(Simulator parent)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            float startingHealth = (float)r.NextDouble();
            float startingHunger = 0.6f * (float)r.NextDouble();
            float startingMetabolism = 0.09f * (float)r.NextDouble();
            float startingHungerDamageThreshold = 0.4f * (float)r.NextDouble();
            float startingHungerImpact = -0.04f * (float)r.NextDouble();


            Dictionary<int, Need> needs = new Dictionary<int, Need>();
            needs.Add(Need.ID_HEALTH, new Need(Need.ID_HEALTH, startingHealth, 0, new TerminateTriggerEffect(parent, 0f, 0.001f)));
            Need hunger = new Need(Need.ID_HUNGER, startingHunger, startingMetabolism, new ModifyNeedEffect(needs[Need.ID_HEALTH], startingHungerDamageThreshold, startingHungerImpact));
            needs.Add(Need.ID_HUNGER, hunger);
            return needs;
        }
    }

}
