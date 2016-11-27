﻿using System;

namespace Simulaton.Simulation
{
    class RequirePropertyAmount : AbilityRequirement
    {
        private Life target;
        private int propertyId;
        private float threshold;
        private Func<float, float, bool> condition;

        public RequirePropertyAmount(Life target, int propertyId, float threshold, Func<float, float, bool> condition)
        {
            this.target = target;
            this.propertyId = propertyId;
            this.threshold = threshold;
            this.condition = condition;
        }

        public bool IsFulfilled()
        {
            float valueOfProperty;
            bool exists = target.TryGetPropertyValue(propertyId, out valueOfProperty);
            Logger.PrintInfo(this, "Comparing "+ Property.Name[propertyId] + " " + Logger.FloatToPercent(valueOfProperty) + " with threshold " + Logger.FloatToPercent(threshold));
            return exists ? condition(valueOfProperty, threshold) : false;
        }
    }
}
