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

        private float magnitude;
        private int versatilityLevel;
        private HashSet<int> satisfiableNeedIds;

        public SatisfyConsequence(int versatilityLevel, float magnitude)
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
            target.ModifyNeed(needIdTrigger, magnitude);
        }
    }
}
