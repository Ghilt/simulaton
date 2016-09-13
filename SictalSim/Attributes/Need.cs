using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class Need
    {
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

        public void Tick()
        {
            float outOfLimit = rate > 0 ? 1 : 0;
            float newValue = amount + rate;
            amount = newValue < outOfLimit ? newValue : outOfLimit;
        }

        internal void Affect(Dictionary<int, Basis> attributes)
        {
            foreach (Effect effect in effects.Values)
            {
                Basis basis;
                if (attributes.TryGetValue(effect.getBasisId(), out basis))
                {
                    effect.modifyBasis(basis, amount);
                } else
                {
                    // No effect for now
                }

            }
        }

        public void Satisfy(int quantity)
        {
            amount -= quantity;
        }

        public override string ToString()
        {
            return amount * 100 + "%";
        }

    }

    class NeedFactory
    {
        public static Need CreateBasicNeed(int id)
        {
            Dictionary<int, Effect> effects = new Dictionary<int, Effect>();
            effects.Add(Basis.ID_HEALTH, new Effect(Basis.ID_HEALTH, 0.2f, 0.01f));
            return new Need(id, 0.5f, 0.01f, effects);
        }
    }

}
