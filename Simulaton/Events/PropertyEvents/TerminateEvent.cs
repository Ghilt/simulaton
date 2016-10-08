using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class TerminateEvent : PropertyEvent
    {
        private float lowerBound;
        private float upperBound;
        private Property source;
        private Entity target;

        public TerminateEvent(Property source, Entity target, float triggerValue, float margin)
        {
            this.source = source;
            this.target = target;
            this.lowerBound = triggerValue - margin;
            this.upperBound = triggerValue + margin;
        }

        public void Trigger()
        {
            if (source.amount > lowerBound  && source.amount < upperBound)
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
