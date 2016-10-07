using Simulaton.Events;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Ability
    {
        public const int ID_SEARCH = 0;
        public const int ID_SLEEP = 1;

        public const int SATISFY_ANY_ONE = 0;
        public const int SATISFY_SPECIFIC = 1;
        public const int SATISFY_GROUP_SPECIFIC = 2;

        public int id { get; private set; }
        private List<AbilityEvent> consequences;
        private Life parent;
        private HashSet<int> satisfiableNeedIds;

        public Ability(int id, Life parent)
        {
            this.id = id;
            this.parent = parent;
            this.consequences = new List<AbilityEvent>();
            this.satisfiableNeedIds = new HashSet<int>();
        }

        internal List<EvaluableResult> GetPrediction(int targetId)
        {
            List<EvaluableResult> impact = new List<EvaluableResult>();
            foreach (AbilityEvent consequence in consequences)
            {
                impact.Add(consequence.EvaluateResult(targetId));
            }
            return impact;
        }

        public void AddsatisfiableNeed(int needId)
        {
            satisfiableNeedIds.Add(needId);
        }


        public void AddConsequence(AbilityEvent consequence)
        {
            consequences.Add(consequence);
        }

        internal void Execute(int targetId)
        {
            Logger.PrintInfo(this, "Do " + Logger.Ability[id]);

            foreach (AbilityEvent consequence in consequences)
            {
                consequence.Trigger(parent, targetId);
            }
        }

        internal bool Satisfies(int id)
        {
            return satisfiableNeedIds.Contains(id);
        }
    }
}
