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
            Life h = new Life(0, new BiologicalNeedFactory(), new Location(r, 50, 50));
            Life h2 = new Life(0, new BiologicalNeedFactory(), new Location(r, 20, 10));
            Life h3 = new Life(0, new BiologicalNeedFactory(), new Location(r, 10, 20));
            Life h4 = new Life(0, new BiologicalNeedFactory(), new Location(r, 40, 10));
            Life h5 = new Life(0, new BiologicalNeedFactory(), new Location(r, 10, 20));

            Ability search = new Ability(Ability.ID_SEARCH, h);
            Interval searchPower = new Interval(0.0f, 0.4f, -1);
            SatisfyConsequence finding = new SatisfyConsequence(SatisfyConsequence.SATISFY_SPECIFIC, searchPower, h.GetLocation());
            finding.AddsatisfiableNeed(Need.ID_NOURISHMENT);
            search.AddConsequence(finding);

            Ability sleep = new Ability(Ability.ID_SLEEP, h);
            Interval sleepPower = new Interval(0.0f, 0.2f);
            SatisfyConsequence asleep = new SatisfyConsequence(SatisfyConsequence.SATISFY_SPECIFIC, sleepPower, null);
            asleep.AddsatisfiableNeed(Need.ID_ENERGY);
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
