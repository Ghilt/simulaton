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

        public const int VERSATILITY_ALL = 0;
        public const int VERSATILITY_SPECIFIC = 1;

        private Interval magnitude;
        private int versatilityLevel;
        private HashSet<int> satisfiableNeedIds;

        public SatisfyConsequence(int versatilityLevel, Interval magnitude)
        {
            this.versatilityLevel = versatilityLevel;
            this.magnitude = magnitude;
            satisfiableNeedIds = new HashSet<int>();
        }

        public void AddsatisfiableNeed(int needId)
        {
            satisfiableNeedIds.Add(needId);
        }

        public bool DoesSatisfyNeed(int needId)
        {
            switch (versatilityLevel) { 
                case VERSATILITY_ALL:
                    return true;
                case VERSATILITY_SPECIFIC:
                    return satisfiableNeedIds.Contains(needId);
                default:
                    return false;
            }
        }

        public void Trigger(Life target, int needIdTrigger)
        {
            float amount = magnitude.value;
            Logger.PrintInfo("Searching for: $, found " + ((int)(amount * 100) + "%"), needIdTrigger);
            target.ModifyNeed(needIdTrigger, amount);
        }
    }
}
