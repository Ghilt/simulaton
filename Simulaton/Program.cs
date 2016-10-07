using Simulaton.Attributes;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            Region r = new Region(0, 100, 100);
            Life h = new Life(0, new BiologicalPropertyFactory(), new Location(r, 50, 50));
            Life h2 = new Life(0, new BiologicalPropertyFactory(), new Location(r, 20, 10));
            Life h3 = new Life(0, new BiologicalPropertyFactory(), new Location(r, 10, 20));
            Life h4 = new Life(0, new BiologicalPropertyFactory(), new Location(r, 40, 10));
            Life h5 = new Life(0, new BiologicalPropertyFactory(), new Location(r, 10, 20));

            Ability search = new Ability(Ability.ID_SEARCH, h);
            search.AddsatisfiableNeed(Property.ID_NOURISHMENT);
            Interval searchPower = new Interval(0.0f, 0.4f, -1);
            SatisfyConsequence finding = new SatisfyConsequence(searchPower, h.GetLocation());
            search.AddConsequence(finding);

            Ability sleep = new Ability(Ability.ID_SLEEP, h);
            sleep.AddsatisfiableNeed(Property.ID_ENERGY);
            Interval sleepPower = new Interval(0.0f, 0.2f);
            SatisfyConsequence asleep = new SatisfyConsequence(Property.ID_ENERGY, sleepPower, null);
            sleep.AddConsequence(asleep);

            h.AddAbility(search);
            h.AddAbility(sleep);
            //engine.AddSimulator(r);
            engine.AddSimulator(h);
            //engine.AddSimulator(h2);
            //engine.AddSimulator(h3);
            //engine.AddSimulator(h4);
            //engine.AddSimulator(h5);
            engine.Start();
        }
    }
}
