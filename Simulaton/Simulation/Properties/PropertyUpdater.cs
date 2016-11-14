using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class PropertyUpdater
    {

        public Property property { get; private set; }
        private float rate;
        private float effectImportance;
        private List<PropertyEvent> effects;

        public PropertyUpdater(Property property, float rate)
        {
            this.property = property;
            this.rate = rate;
            this.effectImportance = 0.0f;
            effects = new List<PropertyEvent>();
        }

        public void OnTick()
        {
            property.ModifyAmount(rate);
            Logger.PrintInfo(this,  "Tick " + Property.Name[property.id] + " " + Logger.FloatToPercentWithSign(rate));
            TriggerEvents();
        }

        private void TriggerEvents()
        {
            foreach (PropertyEvent effect in effects)
            {
                effect.Trigger();
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
            return effectImportance * (1.0f - property.amount);
        }
    }



}
