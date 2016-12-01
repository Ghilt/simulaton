using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulaton.Simulation.AttachedEntitiesList;

namespace Simulaton.Simulation
{
    public abstract class ProteanEntity : Entity
    {
        public Dictionary<int, Property> properties { private set; get; }
        public PropertyUpdaters propertyUpdaters { private set; get; }
        private Location location;
        public AttachedEntitiesList attachedEntities { private set; get; }

        public ProteanEntity(int ticksBirth, string name, Location location) : base(ticksBirth, name)
        {
            this.properties = new Dictionary<int, Property>();
            this.propertyUpdaters = new PropertyUpdaters();
            this.location = location;
            this.attachedEntities = new AttachedEntitiesList();
        }

        public void AddProperty(Property property)
        {
            properties.Add(property.id, property);
        }

        internal Property GetProperty(int propertyId) // Really want to get rid of this accessor
        {
            return properties[propertyId];
        }

        public void AddPropertyUpdater(PropertyUpdater propertyUpdater)
        {
            propertyUpdaters.Add(propertyUpdater);
        }

        internal bool TryGetPropertyValue(int propertyId, out float value)
        {
            Property property;
            bool exist = properties.TryGetValue(propertyId, out property);
            value = exist ? property.amount : 0;
            return exist;
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

        public void PrintProperties()
        {
            Logger.PrintInfo(this, "_________________________________________");
            Logger.PrintInfo(this, name + ", at:" + location.ToString());
            string info = "";
            foreach (Property n in properties.Values)
            {
                info += " " + Property.Name[n.id] + ": " + Logger.FloatToPercent(properties[n.id].amount);
            }
            Logger.PrintInfo(this, info);
        }

        internal Location GetLocation()
        {
            return location;
        }

        internal void SetLocation(Location location)
        {
            if (this.location == location || location == null)
            {
                throw new ArgumentException("Setting location to " + location == null ? "Null" : "old location");
            }
            this.location.OnExit(this);
            this.location = location;
            this.location.OnEnter(this);
        }

        internal void Attach(ProteanEntity proteanEntity) // visitor pattern is confusing, why would this be preferred over anti-patterns
        {
            proteanEntity.OnSelfAttached(attachedEntities.attacher);
        }

        internal void Detach(ProteanEntity proteanEntity)
        {
            proteanEntity.OnSelfAttached(attachedEntities.detacher);
        }

        public abstract void OnSelfAttached(AttachDelegate attachDelegate);

    }
}
