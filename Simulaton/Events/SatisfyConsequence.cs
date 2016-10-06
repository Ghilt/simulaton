using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    class SatisfyConsequence : Consequence
    {
        private const int SATISFY_ANY = -1;

        private Resource resource;
        private Interval magnitude;
        private int needSatisfied;

        public SatisfyConsequence(int needSatisfied, Interval magnitude, Resource resource)
        {
            this.resource = resource;
            this.magnitude = magnitude;
            this.needSatisfied = needSatisfied;
        }

        public SatisfyConsequence(Interval magnitude, Resource resource)
        {
            this.resource = resource;
            this.magnitude = magnitude;
            this.needSatisfied = SATISFY_ANY;
        }

        public void Trigger(Life owner, int needIdTrigger)
        {
            int realTarget = needSatisfied == SATISFY_ANY  ? needIdTrigger : needSatisfied;

            float amount = extractFromSource(realTarget);
            owner.ModifyNeed(realTarget, amount);
            Logger.PrintInfo(this, Logger.Need[realTarget] + ", +" + ((int)(amount * 100) + "%"));
        }

        private float extractFromSource(int needId)
        {
            float amountSatisfiedModifier = (resource == null) ? 1f * magnitude.NextFloat() : resource.Extract(needId) * magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

        public float getMagnitude()
        {
            return magnitude.getPowerLevel();
        }

        public EvaluableResult EvaluateEffectiveness(int targetNeedId)
        {
            int realTarget = needSatisfied == SATISFY_ANY ? targetNeedId : needSatisfied;
            return new EvaluableResult(targetNeedId, getMagnitude());
        }
    }
}
