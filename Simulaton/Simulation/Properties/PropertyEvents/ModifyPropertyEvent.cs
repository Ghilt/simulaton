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
        private PropertyUpdater source;
        private Property target;
        private float importance;
        TransformFunction<float, float> function;

        public ModifyPropertyEvent(PropertyUpdater source, Property target, float importance, TransformFunction<float, float> function)
        {
            this.source = source;
            this.target = target;
            this.importance = importance;
            this.function = function;
        }

        public ModifyPropertyEvent(PropertyUpdater source, Property target, float importance, Func<float, float> function)
            : this(source, target, importance, new GenericTransform(function))
        {
        }

        public void Trigger()
        {
            float modifyBy = function.Transform(source.property.amount);
            Logger.PrintInfo(this, "\t modify " + Logger.FloatToPercent(source.property.amount) + " " + Property.Name[source.property.id] + " -> " + Logger.FloatToPercentWithSign(modifyBy) + " " + Property.Name[target.id]);
            target.ModifyAmount(modifyBy);
        }

        public float GetImportance()
        {
            return importance;
        }
    }
}
