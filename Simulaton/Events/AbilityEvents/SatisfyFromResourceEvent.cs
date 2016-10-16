using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    class SatisfyFromResourceEvent : SatisfyEvent
    {
        private const int SATISFY_ANY = -1;

        private Resource resource;

        public SatisfyFromResourceEvent(Life target, int propertySatisfied, Interval magnitude, Resource resource) : base(target, propertySatisfied, magnitude)
        {
            this.resource = resource;
        }

        public SatisfyFromResourceEvent(Life target, Interval magnitude, Resource resource) : this(target, SATISFY_ANY, magnitude, resource) { }

        public override void Trigger(int propertyIdTrigger)
        {
            int realTarget = propertySatisfied == SATISFY_ANY ? propertyIdTrigger : propertySatisfied;

            float amount = extractFromSource(realTarget);
            target.ModifyProperty(realTarget, amount);
            Logger.PrintInfo(this, "\t " + Logger.Property[realTarget] + ", +" + Logger.FloatToPercent(amount));
        }

        private float extractFromSource(int propertyId)
        {
            float amountSatisfiedModifier = (resource == null) ? 1f * magnitude.NextFloat() : resource.Extract(propertyId) * magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

    }
}
