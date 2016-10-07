﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulaton.Attributes;

namespace Tests
{
    [TestClass]
    public class TestNeeds
    {

        Property testTarget;
        ModifyPropertyEffect effect;

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
            testTarget = new Property(0, 1f, 0f);
            effect = new ModifyPropertyEffect(testTarget, testTarget, -1f, 1f, 0.2f, ((x, threshold) => x < threshold));
            testTarget.AddEffect(effect);

            effect.OnTrigger();
            Assert.AreEqual(1.0f, testTarget.amount);
        }

        [TestMethod]
        public void TestGetImportance()
        {
            float amount = 0.2f;
            testTarget = new Property(0, amount, 0f);
            float highestImportance = 0.5f;
            Effect effect1 = new ModifyPropertyEffect(testTarget, testTarget, -1f, 0.3f, 0.2f, ((x, threshold) => x > threshold));
            Effect effect2 = new ModifyPropertyEffect(testTarget, testTarget, -1f, 0.4f, 0.2f, ((x, threshold) => x > threshold));
            Effect effect3 = new ModifyPropertyEffect(testTarget, testTarget, -1f, highestImportance, 0.2f, ((x, threshold) => x > threshold));
            testTarget.AddEffect(effect1);
            testTarget.AddEffect(effect2);
            testTarget.AddEffect(effect3);

            Assert.AreEqual((1 - amount) * highestImportance, testTarget.GetImportance());
        }

    }
}
