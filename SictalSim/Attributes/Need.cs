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
        private float effectImportance;
        private List<Effect> effects;
        private TerminateEffect terminateTriggerEffect;


        public Need(int id, float amount, float rate, List<Effect> effects)
        {
            this.id = id;
            this.amount = amount;
            this.rate = rate;
            this.effects = effects;
            foreach (Effect effect in effects)
            {
                this.effectImportance *= effect.GetImportance(); 
            }
        }

        public Need(int id, float amount, float rate, Effect effect)
            : this(id, amount, rate, new List<Effect>() { effect })
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

        internal float getImportance()
        {
            return effectImportance*amount;
        }
    }



}
