using Simulaton.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Brain
    {

        public Brain()
        {

        }

        public void MakeDecision(Properties properties, Abilities abilities)
        {

            Property pressingDesire = properties.getMostImportantProperty();
            Logger.PrintInfo(this, "Most pressing need- " + Logger.Property[pressingDesire.id]);
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
                Logger.PrintInfo(this, Logger.Ability[action.id] + " is evaluated as: ");
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
                Logger.PrintInfo(this, "Most fitting action- " + Logger.Ability[toDo.id] + " to get " + Logger.Property[willActUpon.id]);
                toDo.Execute(willActUpon.id);
            }
            else
            {
                Logger.PrintInfo(this, "No available action found.");
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
                Logger.PrintInfo(this, "\t("+ Logger.Property[result.propertyId] + " effect) " + Logger.FloatToPercent(properties[result.propertyId].GetImportance()) + " * " + Logger.FloatToPercent(result.magnitude)  + " = "+ Logger.FloatToPercentWithSign(change));
            }
            return value;
        }
    }

}
