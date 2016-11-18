using Simulaton.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class AbilityModifier
    {
        private int propertyId;
        private TransformFunction<float, float> function;

        public AbilityModifier(int propertyId, TransformFunction<float, float> function)
        {
            this.propertyId = propertyId;
            this.function = function;
        }

        internal float GetModification(Life target)
        {
            float valueOfProperty;
            bool exists = target.TryGetPropertyValue(propertyId, out valueOfProperty);
            
            if(exists){
                return 1;
            }
            else
            {
                return function.Transform(valueOfProperty);
            }
        }
    }
}
