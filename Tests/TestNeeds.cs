using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SictalSim.Attributes;

namespace Tests
{
    [TestClass]
    public class TestNeeds
    {

        Need testTarget;
        ModifyNeedEffect effect;

        //[TestInitialize]
        //public void TestModifyNeedEffectOnTrigger()
        //{
        //    testTarget = new Need(0, 1f, 0f);
        //    effect = new ModifyNeedEffect(testTarget, 0.2f, 1f, 1f);
        //    testTarget.addEffect(effect);
        //}

        [TestMethod]
        public void TestModifyNeedEffectOnTrigger()
        {
            testTarget = new Need(0, 1f, 0f);
            effect = new ModifyNeedEffect(testTarget, 0.2f, 1f, 1f);
            testTarget.addEffect(effect);

            effect.OnTrigger(testTarget);
            Assert.AreEqual(1.0f, testTarget.amount);
        }
    }
}
