using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Mechanics;
using Simulaton.Mechanics.ValueTransformFunctions;

namespace Simulaton.Simulation
{
    public class ModifyPropertyEvent : PropertyEvent
    {
        private float magnitude;
        private PropertyUpdater source;
        private PropertyUpdater target;
        private float importance;
        TransformFunction<float, float> function;

        public ModifyPropertyEvent(PropertyUpdater source, PropertyUpdater target, float magnitude, float importance, TransformFunction<float, float> function)
        {
            this.source = source;
            this.target = target;
            this.magnitude = magnitude;
            this.importance = importance;
            this.function = function;
        }

        public ModifyPropertyEvent(PropertyUpdater source, PropertyUpdater target, float magnitude, float importance, Func<float, float> function)
            : this(source, target, magnitude, importance, new GenericTransform(function))
        {
        }

        internal int getpropertyId()
        {
            return target.property.id;
        }

        public void Trigger()
        {
            float modifyBy = function.Transform(source.property.amount);
            Logger.PrintInfo(this, "\t cause modify " + Property.Name[target.property.id] + " " + Logger.FloatToPercentWithSign(modifyBy));
            target.property.ModifyAmount(modifyBy);
        }

        public float GetImportance()
        {
            return importance;
        }
    }
}
