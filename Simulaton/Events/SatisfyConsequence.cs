using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;

namespace Simulaton.Attributes
{
    class SatisfyConsequence : Consequence
    {

        public const int SATISFY_ANY_ONE = 0;
        public const int SATISFY_SPECIFIC = 1;
        public const int SATISFY_GROUP_SPECIFIC = 2;

        private Resource resource;
        private Interval magnitude;
        private int satisfyMode;
        private HashSet<int> satisfiableNeedIds;

        public SatisfyConsequence(int satisfyMode, Interval magnitude, Resource resource)
        {
            this.resource = resource;
            this.satisfyMode = satisfyMode;
            this.magnitude = magnitude;
            satisfiableNeedIds = new HashSet<int>();
        }

        public void AddsatisfiableNeed(int needId)
        {
            satisfiableNeedIds.Add(needId);
        }

        public bool DoesSatisfyNeed(int needId)
        {
            switch (satisfyMode) { 
                case SATISFY_ANY_ONE:
                    return true;
                case SATISFY_SPECIFIC:
                case SATISFY_GROUP_SPECIFIC:
                    return satisfiableNeedIds.Contains(needId);
                default:
                    throw new Exception("Developer Exception: DoesSatisfyNeed(), Unknown Satisfy mode: " + satisfyMode);
            }
        }

        public void Trigger(Life owner, int needIdTrigger)
        {

            switch (satisfyMode)
            {
                case SATISFY_ANY_ONE:
                case SATISFY_SPECIFIC:
                    float amount = extractFromSource(needIdTrigger);
                    owner.ModifyNeed(needIdTrigger, amount);
                    Logger.PrintInfo("Using action to fix: $, found " + ((int)(amount * 100) + "%"), needIdTrigger);
                    break;
                case SATISFY_GROUP_SPECIFIC:
                    foreach (int needId in satisfiableNeedIds)
                    {
                        owner.ModifyNeed(needIdTrigger, extractFromSource(needId));
                    }
                    break;
                default:
                    throw new Exception("Developer Exception: Trigger(), Unknown Satisfy mode " + satisfyMode);
            }

        }

        private float extractFromSource(int needId)
        {
            float amountSatisfiedModifier = (resource == null) ? 1f*magnitude.NextFloat() : resource.Extract(needId) * magnitude.NextFloat();
            return amountSatisfiedModifier;
        }
    }
}
