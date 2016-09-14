using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class ModifyNeedEffect : Effect
    {
        private float magnitude;
        private float threshold;
        private Need target;

        public ModifyNeedEffect(Need target, float thresholdPercentage, float magnitude)
        {
            this.target = target;
            this.threshold = thresholdPercentage;
            this.magnitude = magnitude;
        }

        internal int getNeedId()
        {
            return target.id;
        }

        public void OnTrigger(Need source)
        {
            if (source.amount < threshold)
            {
                target.Modify(magnitude);
            }
            else if (source.amount > 1 - threshold)
            {
                target.Modify(-magnitude);
            }
        }
    }
}
