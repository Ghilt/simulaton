using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class TerminateTriggerEffect : Effect
    {
        private float lowerBound;
        private float upperBound;
        private Simulator target;

        public TerminateTriggerEffect(Simulator target, float triggerValue, float margin)
        {
            this.target = target;
            this.lowerBound = triggerValue - margin;
            this.upperBound = triggerValue + margin;
        }

        public void OnTrigger(Need source)
        {
            if (source.amount > lowerBound  && source.amount < upperBound)
            {
                target.Terminate();

            }
        }
    }
}
