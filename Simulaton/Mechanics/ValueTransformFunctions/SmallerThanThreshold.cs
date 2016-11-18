using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics.ValueTransformFunctions
{
    class SmallerThanThreshold : GenericTransform
    {

        public SmallerThanThreshold(float threshold, float modifyBy) : base((x => (x < threshold) ? modifyBy : 0f))
        {
        }
    }
}
