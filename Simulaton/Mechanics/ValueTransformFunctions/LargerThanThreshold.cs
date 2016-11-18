using Simulaton.Mechanics.ValueTransformFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics
{
    class LargerThanThreshold : GenericTransform
    {
        public LargerThanThreshold(float threshold, float aboveThresholdValue, float belowThresholdValue = 0f)
            : base(x => (x > threshold) ? aboveThresholdValue : belowThresholdValue)
        {
        }

    }
}
