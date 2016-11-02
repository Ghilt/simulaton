using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulaton.Attributes;
using Simulaton.Simulation;

namespace Tests
{
    [TestClass]
    public class TestNeeds
    {

        Need testTarget;
        ModifyPropertyEvent effect;

        //[TestInitialize]
        //public void TestModifyNeedEffectOnTrigger()
        //{
        //    DebugSetup setup = new DebugSetup();
        //    setup.SetupTestEnvironment();
        //}

        [TestMethod]
        public void TestModifyPropertyEventOnTrigger()
        {
            Property p = new Property(null, 0, 1);
            testTarget = new Need(p, 0f);
            effect = new ModifyPropertyEvent(testTarget, testTarget, -1f, 1f, 0.2f, ((x, threshold) => x < threshold));
            testTarget.AddEffect(effect);

            effect.Trigger();
            Assert.AreEqual(1.0f, testTarget.property.amount);
        }

        [TestMethod]
        public void TestGetImportance()
        {
            float amount = 0.2f;
            Property p = new Property(null, 0, amount);
            testTarget = new Need(p, 0f);
            float highestImportance = 0.5f;
            PropertyEvent effect1 = new ModifyPropertyEvent(testTarget, testTarget, -1f, 0.3f, 0.2f, ((x, threshold) => x > threshold));
            PropertyEvent effect2 = new ModifyPropertyEvent(testTarget, testTarget, -1f, 0.4f, 0.2f, ((x, threshold) => x > threshold));
            PropertyEvent effect3 = new ModifyPropertyEvent(testTarget, testTarget, -1f, highestImportance, 0.2f, ((x, threshold) => x > threshold));
            testTarget.AddEffect(effect1);
            testTarget.AddEffect(effect2);
            testTarget.AddEffect(effect3);

            Assert.AreEqual((1 - amount) * highestImportance, testTarget.GetImportance());
        }

    }
}
