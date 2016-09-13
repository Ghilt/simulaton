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
        private int basisId;

        public Effect(int basisId, float thresholdPercentage, float magnitude)
        {
            this.basisId = basisId;
            this.threshold = thresholdPercentage;
            this.magnitude = magnitude;
        }

        internal void modifyBasis(Basis target, float amount)
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

        internal int getBasisId()
        {
            return basisId;
        }
    }
}
