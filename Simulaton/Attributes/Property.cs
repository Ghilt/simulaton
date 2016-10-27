using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public partial class Property
    {

        public int id { get; private set; }
        public float amount { get; private set; } // between 0 and 1
        private float rate;
        private float effectImportance;
        private List<PropertyEvent> effects;

        public Property(int id, float amount, float rate)
        {
            this.id = id;
            this.amount = amount;
            this.rate = rate;
            this.effectImportance = 0.0f;
            effects = new List<PropertyEvent>();
        }

        public void OnTick()
        {
            float relevantLimit = rate > 0 ? 1 : 0;
            float newValue = amount + rate;
            bool isOutOfLimit = relevantLimit == 1 ? newValue < relevantLimit : newValue > relevantLimit;
            amount = isOutOfLimit ? newValue : relevantLimit;
            Logger.PrintInfo(this,  "Tick " + Property.Name[id] + " " + Logger.FloatToPercentWithSign(rate));
            TriggerEvents();
        }

        private void TriggerEvents()
        {
            foreach (PropertyEvent effect in effects)
            {
                effect.Trigger();
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

        public void AddEffect(PropertyEvent effect)
        {
            effects.Add(effect);
            if (effect.GetImportance() > effectImportance)
            {
                this.effectImportance = effect.GetImportance();
            }
        }

        public float GetImportance()
        {
            return effectImportance * (1.0f - amount);
        }
    }



}
