using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class ModifyPropertyEvent : PropertyEvent
    {
        private float magnitude;
        private Need source;
        private Need target;
        private float importance;
        private Func<float, bool> condition;

        public ModifyPropertyEvent(Need source, Need target, float magnitude, float importance, Func<float, bool> condition)
        {
            this.source = source;
            this.target = target;
            this.magnitude = magnitude;
            this.importance = importance;
            this.condition = condition;
        }

        internal int getpropertyId()
        {
            return target.property.id;
        }

        public void Trigger()
        {
            if (condition(source.property.amount))
            {
                target.property.ModifyAmount(magnitude);
            }
        }

        public float GetImportance()
        {
            return importance;
        }
    }
}
