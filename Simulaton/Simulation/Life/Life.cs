using System;
using System.Collections.Generic;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    public class Life : ProteanEntity
    {

        public Brain brain { private set; get; }
        private ItemManager itemManager;
        private Abilities actions;

        public Life(int ticksBirth, String name, Location location)
            : base(ticksBirth, name, location)
        {
            this.brain = new Brain(this);
            this.itemManager = new ItemManager(this);
            this.actions = new Abilities();
            location.OnEnter(this);
        }

        public void AddAbility(Ability action)
        {
            actions.Add(action.id, action);
        }

        public override void OnTick()
        {
            AddSummary(CreateCurrentSummary());
            PrintProperties();
            GetLocation().TryMove(0.5f); // todo remove
            propertyUpdaters.OnTick();
            brain.MakeDecision(propertyUpdaters, actions);
        }

        public override void OnEvent(Event exteriorEvent)
        {
            exteriorEvent.Handle(this);
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

        internal bool AccessToItem(int itemId)
        {
            return itemManager.AccessToItem(itemId);
        }

        internal bool TryGetItem(int itemId, out Item item)
        {
            return itemManager.TryGetItem(itemId, out item);
        }

        public override void OnAttach(AttachedEntitiesList attachedEntities)
        {
            attachedEntities.Attach(this); 
        }

        public override void OnDetach(AttachedEntitiesList attachedEntities)
        {
            attachedEntities.Detach(this);
        }
    }
}
