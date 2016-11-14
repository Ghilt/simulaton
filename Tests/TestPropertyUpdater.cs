using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulaton.Attributes;
using Simulaton.Simulation;

namespace Tests
{
    [TestClass]
    public class TestPropertyUpdaters
    {

        PropertyUpdater testTarget;
        ModifyPropertyEvent effect;

        //[TestInitialize]
        //public void TestModifyPropertyUpdaterEffectOnTrigger()
        //{
        //    DebugSetup setup = new DebugSetup();
        //    setup.SetupTestEnvironment();
        //}

        [TestMethod]
        public void TestModifyPropertyEventOnTrigger()
        {
            Property p = new Property(0, 1);
            testTarget = new PropertyUpdater(p, 0f);
            effect = new ModifyPropertyEvent(testTarget, testTarget, -1f, 1f, (x => x < 0.2f));
            testTarget.AddEffect(effect);

            effect.Trigger();
            Assert.AreEqual(1.0f, testTarget.property.amount);
        }

        [TestMethod]
        public void TestGetImportance()
        {
            float amount = 0.2f;
            Property p = new Property(0, amount);
            testTarget = new PropertyUpdater(p, 0f);
            float highestImportance = 0.5f;
            PropertyEvent effect1 = new ModifyPropertyEvent(testTarget, testTarget, -1f, 0.3f,  (x => x > 0.2f));
            PropertyEvent effect2 = new ModifyPropertyEvent(testTarget, testTarget, -1f, 0.4f, (x => x > 0.2f));
            PropertyEvent effect3 = new ModifyPropertyEvent(testTarget, testTarget, -1f, highestImportance, (x => x > 0.2f));
            testTarget.AddEffect(effect1);
            testTarget.AddEffect(effect2);
            testTarget.AddEffect(effect3);

            Assert.AreEqual((1 - amount) * highestImportance, testTarget.GetImportance());
        }

    }
}
