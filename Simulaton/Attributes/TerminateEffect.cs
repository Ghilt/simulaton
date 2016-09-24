using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class TerminateEffect : Effect
    {
        private float lowerBound;
        private float upperBound;
        private Simulator target;

        public TerminateEffect(Simulator target, float triggerValue, float margin)
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

        public float GetImportance()
        {
            return 1.0f;
        }
    }
}
