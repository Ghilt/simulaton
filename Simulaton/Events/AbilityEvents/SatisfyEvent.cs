using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    class SatisfyEvent : AbilityEvent
    {
        private const int SATISFY_ANY = -1;

        public Interval magnitude { get; private set; }
        public int needSatisfied { get; private set; }

        public SatisfyEvent(int needSatisfied, Interval magnitude)
        {
            this.magnitude = magnitude;
            this.needSatisfied = needSatisfied;
        }

        public SatisfyEvent(Interval magnitude)
        {
            this.magnitude = magnitude;
            this.needSatisfied = SATISFY_ANY;
        }

        public virtual void Trigger(Life owner, int needIdTrigger)
        {
            int realTarget = needSatisfied == SATISFY_ANY  ? needIdTrigger : needSatisfied;

            float amount = extractFromSource(realTarget);
            owner.ModifyProperty(realTarget, amount);
            Logger.PrintInfo(this, Logger.Need[realTarget] + ", +" + ((int)(amount * 100) + "%"));
        }

        private float extractFromSource(int needId)
        {
            float amountSatisfiedModifier = magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

        public float GetMagnitude()
        {
            return magnitude.getPowerLevel();
        }

        public EvaluableResult EvaluateResult(int targetNeedId)
        {
            int realTarget = needSatisfied == SATISFY_ANY ? targetNeedId : needSatisfied;
            return new EvaluableResult(targetNeedId, GetMagnitude());
        }
    }
}
