using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics.ValueTransformFunctions
{
    class GenericTransform : TransformFunction<float, float>
    {
        Func<float, float> function;

        public GenericTransform(Func<float, float> function)
        {
            this.function = function;
        }

        public float Transform(float x)
        {
            float result = function(x);
            Logger.PrintInfo(this, "\t transform " + Logger.FloatToPercentWithSign(x) + " -> " + Logger.FloatToPercentWithSign(result));
            return result;
        }
    }
}
