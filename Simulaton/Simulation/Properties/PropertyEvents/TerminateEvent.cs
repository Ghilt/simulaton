using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    class TerminateEvent : PropertyEvent
    {
        private PropertyUpdater source;
        private Entity target;
        private Func<float, bool> condition;

        public TerminateEvent(PropertyUpdater source, Entity target, Func<float, bool> condition)
        {
            this.source = source;
            this.target = target;
            this.condition = condition;
        }

        public void Trigger()
        {
            if (condition(source.property.amount))
            {
                target.Terminate();
            }
        }

        public float GetImportance()
        {
            return 1.0f;
        }
    }
}
