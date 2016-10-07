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

        public void MakeDecision(Properties needs, Abilities abilities)
        {

            Property pressingDesire = needs.getMostImportantNeed();
            Logger.PrintInfo(this, "Most pressing need- " + Logger.Need[pressingDesire.id]);
            float largestValue = 0.0f;
            Ability toDo = null;
            Property willActUpon = null;
            foreach (Ability action in abilities.Values)
            {
                Property target;
                if (!TryGetBestMatch(needs, action, out target))
                {
                    continue;
                }

                List<EvaluableResult> prediction = action.GetPrediction(target.id);
                Logger.PrintInfo(this, Logger.Ability[action.id] + " is evaluated as: ");
                float value = Evaluate(needs, prediction);

                if (value > largestValue)
                {
                    willActUpon = target;
                    largestValue = value;
                    toDo = action;
                }

            }
            if (toDo != null)
            {
                Logger.PrintInfo(this, "Most fitting action- " + Logger.Ability[toDo.id] + " to get " + Logger.Need[willActUpon.id]);
                toDo.Execute(willActUpon.id);
            }
            else
            {
                Logger.PrintInfo(this, "No available action found.");
            }
        }

        private bool TryGetBestMatch(Properties needs, Ability action, out Property match)
        {
            foreach (Property need in needs.SortedOnImportance())
            {
                if (action.Satisfies(need.id))
                {
                    match = need;
                    return true;
                }
            }
            match = null;
            return false;

        }

        private float Evaluate(Properties needs, List<EvaluableResult> prediction)
        {
            float value = 0;
            foreach (EvaluableResult result in prediction)
            {
                value += needs[result.needId].GetImportance() * result.magnitude;
                Logger.PrintInfo(this, "\t"+ Logger.Need[result.needId] + " increases value by " + needs[result.needId].GetImportance() + " * " +  result.magnitude  + " = "+ value);
            }
            return value;
        }
    }

}
