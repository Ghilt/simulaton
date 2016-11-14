using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    class TerminateEvent : PropertyEvent
    {
        private float lowerBound;
        private float upperBound;
        private PropertyUpdater source;
        private Entity target;

        public TerminateEvent(PropertyUpdater source, Entity target, float triggerValue, float margin)
        {
            this.source = source;
            this.target = target;
            this.lowerBound = triggerValue - margin;
            this.upperBound = triggerValue + margin;
        }

        public void Trigger()
        {
            if (source.property.amount > lowerBound  && source.property.amount < upperBound)
            {
                target.Terminate();
            }
        }

        public float GetImportance()
        {
            return 1.0f;
        }
    }
}
