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

        private int id;
        private float amount; // between 0 and 1
        private float rate;
        private Dictionary<int, Effect> effects;

        public Need(int id, float amount, float rate, Dictionary<int, Effect> effects)
        {
            this.id = id;
            this.amount = amount;
            this.rate = rate;
            this.effects = effects;
        }

        public Need(int id, float amount)
        {
            this.id = id;
            this.amount = amount;
            this.rate = 0;
            this.effects = new Dictionary<int, Effect>();
        }

        public void Tick()
        {
            float relevantLimit = rate > 0 ? 1 : 0;
            float newValue = amount + rate;
            bool isOutOfLimit = relevantLimit == 1 ? newValue < relevantLimit : newValue > relevantLimit;
            amount = isOutOfLimit ? newValue : relevantLimit;
        }

        internal void Affect(Dictionary<int, Need> attributes)
        {
            foreach (Effect effect in effects.Values)
            {
                Need need;
                if (attributes.TryGetValue(effect.getNeedId(), out need))
                {
                    effect.modifyNeed(need, amount);
                } else
                {
                    // No effect for now
                }

            }
        }

        public void Modify(float quantity)
        {
            amount -= quantity;
        }

        public override string ToString()
        {
            return ((int)(amount * 100) + "%");
        }

    }

    class NeedFactory
    {
        public static Need CreateBasicNeed(int id)
        {
            Dictionary<int, Effect> effects = new Dictionary<int, Effect>();
            effects.Add(Need.ID_HEALTH, new Effect(Need.ID_HEALTH, 0.2f, -0.01f));
            return new Need(id, 0.5f, 0.01f, effects);
        }
    }

}
