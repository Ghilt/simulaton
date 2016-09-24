using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class Need
    {
        public const int ID_HEALTH = 0;
        public const int ID_NURISHMENT = 1;

        public int id { get; private set; }
        public float amount { get; private set; } // between 0 and 1
        private float rate;
        private float effectImportance;
        private List<Effect> effects;

        public Need(int id, float amount, float rate, List<Effect> effects)
        {
            this.id = id;
            this.amount = amount;
            this.rate = rate;
            this.effects = effects;
            this.effectImportance = 1.0f;
            foreach (Effect effect in effects)
            {
                this.effectImportance *= effect.GetImportance(); 
            }
        }

        public Need(int id, float amount, float rate, Effect effect) : this(id, amount, rate, new List<Effect>() { effect })
        {
            
        }

        public Need(int id, float amount, float rate) : this(id, amount, rate, new List<Effect>())
        {

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
            amount += quantity;
            if (amount > 1)
            {
                amount = 1;
            }
            else if (amount < 0)
            {
                amount = 0;
            } 
        }

        public override string ToString()
        {
            return ((int)(amount * 100) + "%");
        }

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
            this.effectImportance *= effect.GetImportance();
        }

        internal float GetImportance()
        {
            return effectImportance*(1.0f-amount);
        }
    }



}
