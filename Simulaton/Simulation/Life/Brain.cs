using Simulaton.Events;
using Simulaton.Mechanics;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class Brain
    {
        private Life owner;
        public Brain(Life owner)
        {
            this.owner = owner;
        }

        public void MakeDecision(PropertyUpdaters propertyUpdaters, Abilities abilities)
        {

            PropertyUpdater pressingDesire = propertyUpdaters.getMostImportantPropertyUpdater();
            Logger.PrintInfo(this, "Most pressing propertyUpdater- " + Property.Name[pressingDesire.property.id]);
            float largestValue = 0.0f;
            Ability toDo = null;
            PropertyUpdater willActUpon = null;
            foreach (Ability action in abilities.Values)
            {
                PropertyUpdater target;
                if (!TryGetBestMatch(propertyUpdaters, action, out target))
                {
                    continue;
                }

                List<EvaluableResult> prediction = action.GetPrediction(target.property.id);
                Logger.PrintInfo(this, Ability.Name[action.id] + " is evaluated as: ");
                float value = Evaluate(propertyUpdaters, prediction);

                if (value > largestValue)
                {
                    willActUpon = target;
                    largestValue = value;
                    toDo = action;
                }

            }
            if (toDo != null)
            {
                Logger.PrintInfo(this, "Most fitting action- " + Ability.Name[toDo.id] + " to get " + Property.Name[willActUpon.property.id]);
                toDo.Execute(willActUpon.property.id);
                owner.AddSummary(SummaryFactory.CreateSummary(toDo));
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
            float value = Evaluate(owner.propertyUpdaters, prediction);
            if (value > 0)
            {
                ability.executeInteraction(propertyTargetId, owner);
            }
        }

        private bool TryGetBestMatch(PropertyUpdaters propertyUpdaters, Ability action, out PropertyUpdater match)
        {
            foreach (PropertyUpdater propertyUpdater in propertyUpdaters.SortedOnImportance())
            {
                if (action.Satisfies(propertyUpdater.property.id))
                {
                    match = propertyUpdater;
                    return true;
                }
            }
            match = null;
            return false;

        }

        private float Evaluate(PropertyUpdaters propertyUpdaters, List<EvaluableResult> prediction)
        {
            float value = 0;
            if (prediction.Count == 0)
            {
                Logger.PrintInfo(this, "\tNo effect");
            }
            foreach (EvaluableResult result in prediction)
            {
                float change = propertyUpdaters[result.propertyId].GetImportance() * result.magnitude;
                value += change;
                Logger.PrintInfo(this, "\t(" + Property.Name[result.propertyId] + " effect) " + Logger.FloatToPercent(propertyUpdaters[result.propertyId].GetImportance()) + " * " + Logger.FloatToPercent(result.magnitude) + " = " + Logger.FloatToPercentWithSign(change));
            }
            return value;
        }
    }

}
