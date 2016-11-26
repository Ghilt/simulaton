﻿using Simulaton.Attributes;
using System;
using System.Collections.Generic;
using Simulaton.Events;
using Simulaton.Mechanics;
using Simulaton.Simulation;

namespace Simulaton.Simulation
{
    public class Life : Entity
    {

        private Location location;
        public Brain brain { private set; get; }
        public Dictionary<int, Property> properties { private set; get; }
        public ItemManager itemManager;
        public PropertyUpdaters propertyUpdaters { private set; get; }
        private Abilities actions;

        public Life(int ticksBirth, String name, Location location)
            : base(ticksBirth, name)
        {
            this.location = location;
            this.brain = new Brain(this);
            this.itemManager = new ItemManager(this);
            this.properties = new Dictionary<int, Property>();
            this.propertyUpdaters = new PropertyUpdaters();
            this.actions = new Abilities();
        }

        public void AddPropertyUpdater(PropertyUpdater propertyUpdater)
        {
            propertyUpdaters.Add(propertyUpdater);
        }

        internal bool TryGetPropertyValue(int propertyId, out float value)
        {
            Property property;
            bool exist = properties.TryGetValue(propertyId, out property);
            value = exist ? property.amount: 0;
            return exist;
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id, action);
        }

        public override void OnTick()
        {
            AddSummary(CreateCurrentSummary());
            PrintProperties();
            location.Move(); // todo remove
            propertyUpdaters.OnTick();
            brain.MakeDecision(propertyUpdaters, actions);
        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
        }

        internal void ModifyProperty(int propertyIdTrigger, float magnitude)
        {
            Property toModify;
            properties.TryGetValue(propertyIdTrigger, out toModify);
            if (toModify != null)
            {
                toModify.ModifyAmount(magnitude);
            }
            else
            {
                Logger.PrintInfo(this, "Tried to modify property " + Property.Name[propertyIdTrigger] + " but Life did not have it");
            }
        }

        internal Location GetLocation()
        {
            return location;
        }

        private void PrintProperties()
        {
            Logger.PrintInfo(this, "_________________________________________");
            Logger.PrintInfo(this, name + ", at x: " + location.x + " y: " + location.y);
            string info = "";
            foreach (Property n in properties.Values)
            {
                info += " " + Property.Name[n.id] + ": " + Logger.FloatToPercent(properties[n.id].amount);
            }
            Logger.PrintInfo(this, info);
        }

        public void AddProperty(Property property)
        {
            properties.Add(property.id, property);
        }

        public override void OnTerminate()
        {
            Logger.PrintInfo(this, name + " died");
        }

        private Summary[] CreateCurrentSummary()
        {
            List<Summary> summaries = new List<Summary>();
            foreach (Property p in properties.Values)
            {
                Summary summary = SummaryFactory.CreateSummary(p);
                summaries.Add(summary);
            }
            return summaries.ToArray(); ;
        }

        internal Property GetProperty(int propertyId)
        {
            return properties[propertyId];
        }

        internal bool AccessToItem(int itemId, float amount)
        {
            return itemManager.AccessToItem(itemId, amount);
        }
    }
}
