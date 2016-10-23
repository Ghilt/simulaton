using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    public class SummaryManager
    {
        private const int SAVE_TICKS_HISTORY = 5;
        private Tick tick;
        private Dictionary<int, Dictionary<Entity, List<Summary>>> summaries;


        public void SetTimeTicker(Tick tick)
        {
            summaries = new Dictionary<int, Dictionary<Entity, List<Summary>>>();
            this.tick = tick;
        }

        public void AddSummary(Entity owner, params Summary[] summaryList)
        {
            int currentTick = tick.Current();
            if (!summaries.ContainsKey(currentTick))
            {
                var removeOldTicks = summaries.Keys.Where(timeStamp => currentTick - timeStamp > SAVE_TICKS_HISTORY).ToList();
                foreach (var oldTick in removeOldTicks)
                {
                    summaries.Remove(oldTick);
                }
                summaries.Add(currentTick, new Dictionary<Entity, List<Summary>>());
            }
            if (!summaries[currentTick].ContainsKey(owner))
            {
                summaries[currentTick].Add(owner, new List<Summary>());
            }

            foreach (Summary summary in summaryList)
            {
                summaries[currentTick][owner].Add(summary);
            }
        }

        public Dictionary<Entity, List<Summary>> GetCurrentData()
        {
            Dictionary<Entity, List<Summary>> current;
            if (summaries.TryGetValue(tick.Current(), out current))
            {
                return current;

            } else
            {
                return new Dictionary<Entity, List<Summary>>();
            }
        }
    }
}
