using Simulaton.Events;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class Ability
    {
        public const int ID_SEARCH = 0;
        public const int ID_SLEEP = 1;

        public int id { get; private set; }
        private List<AbilityEvent> consequences;
        private Life parent;
        private HashSet<int> satisfiablePropertyIds;
        private List<Requirement> requirements;

        public Ability(int id, Life parent)
        {
            this.id = id;
            this.parent = parent;
            this.consequences = new List<AbilityEvent>();
            this.requirements = new List<Requirement>();
            this.satisfiablePropertyIds = new HashSet<int>();
        }

        public List<EvaluableResult> GetPrediction(int targetId)
        {
            List<EvaluableResult> impact = new List<EvaluableResult>();
            foreach (Requirement requirement in requirements)
            {
                if (!requirement.IsFulfilled())
                {
                    Logger.PrintInfo(this, Logger.Ability[id] + " not possible, failing requirements");
                    return impact;
                }
            }
            foreach (AbilityEvent consequence in consequences)
            {
                impact.Add(consequence.EvaluateResult(targetId));
            }
            return impact;
        }

        public void AddSatisfiableProperty(int propertyId)
        {
            satisfiablePropertyIds.Add(propertyId);
        }


        public void AddConsequence(AbilityEvent consequence)
        {
            consequences.Add(consequence);
        }

        internal void AddRequirement(Requirement requirement)
        {
            requirements.Add(requirement);
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
            return satisfiablePropertyIds.Contains(id);
        }
    }
}
