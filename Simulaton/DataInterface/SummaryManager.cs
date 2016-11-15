using Simulaton.Simulation;
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
        private Dictionary<int, List<string>> logs;


        public void SetTimeTicker(Tick tick)
        {
            summaries = new Dictionary<int, Dictionary<Entity, List<Summary>>>();
            logs = new Dictionary<int, List<string>>();
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

        internal void AddLogg(string info)
        {
            int currentTick = tick.Current();
            if (!logs.ContainsKey(currentTick))
            {
                var removeOldTicks = logs.Keys.Where(timeStamp => currentTick - timeStamp > SAVE_TICKS_HISTORY).ToList();
                foreach (var oldTick in removeOldTicks)
                {
                    logs.Remove(oldTick);
                }
                logs.Add(currentTick, new List<string>());
            }

            logs[currentTick].Add(info);
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

        public List<string> GetCurrentLogs()
        {
            List<string> current;
            if (logs.TryGetValue(tick.Current(), out current))
            {
                return current;
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
