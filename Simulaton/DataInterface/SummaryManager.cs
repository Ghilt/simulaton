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
        private Dictionary<int, Dictionary<Guid, List<Summary>>> summaries;
        private Dictionary<int, List<string>> logs;


        public void SetTimeTicker(Tick tick)
        {
            summaries = new Dictionary<int, Dictionary<Guid, List<Summary>>>();
            logs = new Dictionary<int, List<string>>();
            this.tick = tick;
        }

        public void AddSummary(Guid ownerGuid, params Summary[] summaryList)
        {
            int currentTick = tick.Current();
            if (!summaries.ContainsKey(currentTick))
            {
                var removeOldTicks = summaries.Keys.Where(timeStamp => currentTick - timeStamp > SAVE_TICKS_HISTORY).ToList();
                foreach (var oldTick in removeOldTicks)
                {
                    summaries.Remove(oldTick);
                }
                summaries.Add(currentTick, new Dictionary<Guid, List<Summary>>());
            }
            if (!summaries[currentTick].ContainsKey(ownerGuid))
            {
                summaries[currentTick].Add(ownerGuid, new List<Summary>());
            }

            foreach (Summary summary in summaryList)
            {
                summaries[currentTick][ownerGuid].Add(summary);
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

        public Dictionary<Guid, List<Summary>> GetCurrentData()
        {
            Dictionary<Guid, List<Summary>> current;
            if (summaries.TryGetValue(tick.Current(), out current))
            {
                return current;

            } else
            {
                return new Dictionary<Guid, List<Summary>>();
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
