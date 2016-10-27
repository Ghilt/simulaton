using Simulaton.Events;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class Brain
    {
        private Life owner;
        public Brain(Life owner)
        {
            this.owner = owner;
        }

        public void MakeDecision(Properties properties, Abilities abilities)
        {

            Property pressingDesire = properties.getMostImportantProperty();
            Logger.PrintInfo(this, "Most pressing need- " + Property.Name[pressingDesire.id]);
            float largestValue = 0.0f;
            Ability toDo = null;
            Property willActUpon = null;
            foreach (Ability action in abilities.Values)
            {
                Property target;
                if (!TryGetBestMatch(properties, action, out target))
                {
                    continue;
                }

                List<EvaluableResult> prediction = action.GetPrediction(target.id);
                Logger.PrintInfo(this, Ability.Name[action.id] + " is evaluated as: ");
                float value = Evaluate(properties, prediction);

                if (value > largestValue)
                {
                    willActUpon = target;
                    largestValue = value;
                    toDo = action;
                }

            }
            if (toDo != null)
            {
                Logger.PrintInfo(this, "Most fitting action- " + Ability.Name[toDo.id] + " to get " + Property.Name[willActUpon.id]);
                toDo.Execute(willActUpon.id);
                owner.AddSummary(new Summary(Summary.TYPE_ABILITY, toDo.id));

            }
            else
            {
                Logger.PrintInfo(this, "No available action found.");
            }
        }

        internal void MakeDecision(int propertyTargetId, InteractionAbility ability)
        {
            Logger.PrintInfo(this, "Interaction request for " + Ability.Name[ability.id]);
            List<EvaluableResult> prediction = ability.GetPrediction(propertyTargetId);
            float value = Evaluate(owner.properties, prediction);
            if (value > 0)
            {
                ability.executeInteraction(propertyTargetId, owner);
            }
        }

        private bool TryGetBestMatch(Properties properties, Ability action, out Property match)
        {
            foreach (Property property in properties.SortedOnImportance())
            {
                if (action.Satisfies(property.id))
                {
                    match = property;
                    return true;
                }
            }
            match = null;
            return false;

        }

        private float Evaluate(Properties properties, List<EvaluableResult> prediction)
        {
            float value = 0;
            if (prediction.Count == 0)
            {
                Logger.PrintInfo(this, "\tNo effect");
            }
            foreach (EvaluableResult result in prediction)
            {
                float change = properties[result.propertyId].GetImportance() * result.magnitude;
                value += change;
                Logger.PrintInfo(this, "\t(" + Property.Name[result.propertyId] + " effect) " + Logger.FloatToPercent(properties[result.propertyId].GetImportance()) + " * " + Logger.FloatToPercent(result.magnitude) + " = " + Logger.FloatToPercentWithSign(change));
            }
            return value;
        }
    }

}
