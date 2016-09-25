using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulaton.Attributes;

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
            effect = new ModifyNeedEffect(testTarget,testTarget, 0.2f, -1f, 1f);
            testTarget.AddEffect(effect);

            effect.OnTrigger();
            Assert.AreEqual(1.0f, testTarget.amount);
        }

        [TestMethod]
        public void TestGetImportance()
        {
            float amount = 0.2f;
            testTarget = new Need(0, amount, 0f);
            Effect effect1 = new ModifyNeedEffect(testTarget, testTarget, 0.2f, -1f, 0.9f);
            Effect effect2 = new ModifyNeedEffect(testTarget, testTarget, 0.2f, -1f, 0.8f);
            Effect effect3 = new ModifyNeedEffect(testTarget, testTarget, 0.2f, -1f, 1f);
            testTarget.AddEffect(effect1);
            testTarget.AddEffect(effect2);
            testTarget.AddEffect(effect3);

            Assert.AreEqual((1-amount)*0.9f*0.8f*1f, testTarget.GetImportance());
        }

    }
}
