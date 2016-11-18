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
        public LargerThanThreshold(float threshold, float modifyBy) : base (x => (x > threshold) ? modifyBy : 0f)
        {
        }

    }
}
