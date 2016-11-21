using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics.ValueTransformFunctions
{
    class LinearTransform : GenericTransform
    {

        public LinearTransform(float valueAtZero, float valueAtOne)
            : base(x => valueAtZero + x * (valueAtOne - valueAtZero))
        {
            
        }

    }
}
