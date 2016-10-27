using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulaton.Attributes;
using Simulaton.Simulation;
using Simulaton;
using Simulaton.Events;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestAbility
    {

        [TestMethod]
        public void TestGetPrediction()
        {
            int PROPERTY_SATISFIED = 1;
            Life target = new Life(0,"TesterName", null);
            Ability ability = new Ability(PROPERTY_SATISFIED, target);
            AbilityEvent c1 = new SatisfyEvent(new Interval(0.0f, 1.0f));
            AbilityEvent c2 = new SatisfyEvent(new Interval(0.0f, 0.2f));
            AbilityEvent c3 = new SatisfyEvent(PROPERTY_SATISFIED+1, new Interval(0.0f, 0.4f));
            ability.AddConsequence(c1);
            ability.AddConsequence(c2);
            ability.AddConsequence(c3);

            List<EvaluableResult> results = ability.GetPrediction(1);

            Assert.AreEqual(3, results.Count);
            float aggregatedResult = 0;
            foreach (EvaluableResult result in results)
            {
                aggregatedResult += result.magnitude;
            }
            Assert.AreEqual(0.5f + 0.1f + 0.2f, aggregatedResult);
        }


    }
}
