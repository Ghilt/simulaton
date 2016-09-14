using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class Effect
    {
        private float magnitude;
        private float threshold;
        private int needId;

        public Effect(int needId, float thresholdPercentage, float magnitude)
        {
            this.needId = needId;
            this.threshold = thresholdPercentage;
            this.magnitude = magnitude;
        }

        internal void modifyNeed(Need target, float amount)
        {
            if (amount < threshold)
            {
                target.Modify(magnitude);
            }
            else if (amount > 1 - threshold)
            {
                target.Modify(-magnitude);
            } 
        }

        internal int getNeedId()
        {
            return needId;
        }
    }
}
