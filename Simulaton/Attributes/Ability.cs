using Simulaton.Events;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public partial class Ability
    {

        public int id { get; private set; }
        public Life parent { get; private set; }
        public List<AbilityEvent> consequences { get; private set; }
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
                    Logger.PrintInfo(this, Ability.Name[id] + " not possible, failing requirements");
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

        internal virtual void Execute(int targetPropertyId)
        {
            Logger.PrintInfo(this, "Do " + Property.Name[id]);

            foreach (AbilityEvent consequence in consequences)
            {
                consequence.Trigger(targetPropertyId, parent);
            }
        }

        internal bool Satisfies(int id)
        {
            return satisfiablePropertyIds.Contains(id);
        }
    }
}
