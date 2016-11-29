using Simulaton.ConsoleUi;
using Simulaton.Mechanics;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class ConsolePresenter : IObserver<SummaryManager>
    {
        private int windowHeight;
        private int windowWidth;
        private Controls controls;

        public ConsolePresenter(int windowWidth, int windowHeight, SummaryManager summaryManager)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.controls = new Controls(this, windowWidth, windowHeight);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Logger.InjectSummaryManager(summaryManager);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SummaryManager data)
        {
            controls.NextTick(data);
        }

        public void RenderCurrentTick(SummaryManager data, int offset)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            var listData = data.GetCurrentData().ToList();
            ConsoleFrame ui = CreateFullFrame();
            ui.InsertEarliest(controls.GetControlsFrame());
            if (offset > listData.Count) offset = 0;

            for (int i = offset; i < listData.Count; i++ )
            {
                EntityUiFrame frame = new EntityUiFrame(listData[i].Key.ToString(), listData[i].Value, 50, 10);
                ui.InsertEarliest(frame);
            }
            for (int i = 0; i < offset; i++)
            {
                EntityUiFrame frame = new EntityUiFrame(listData[i].Key.ToString(), listData[i].Value, 50, 10);
                ui.InsertEarliest(frame);
            }

            string render = ui.GetFrameRender();
            Console.Write(render);
            Console.SetCursorPosition(0, windowHeight - 1);
            controls.UserAction(data);
        }

        public void ShowLog(SummaryManager data, int offset)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            int logStartXOffset = 0;
            var listData = data.GetCurrentLogs();
            ConsoleFrame ui = CreateFullFrame();
            ui.InsertEarliest(controls.GetControlsFrame());

            for (int i  = 0; i < listData.Count; i++)
            {
                if (i < offset)
                {
                    continue;
                }
                bool success = ui.InsertEarliestAlongColumn(listData[i], logStartXOffset);
                if (!success)
                {
                    ui.InsertEarliestAlongColumn("Logg too long", logStartXOffset);
                }
            }

            string render = ui.GetFrameRender();
            Console.Write(render);
            Console.SetCursorPosition(0, windowHeight - 1);
            controls.UserAction(data);
        }

        public ConsoleFrame CreateFullFrame()
        {
            return new ConsoleFrame(windowWidth - 1, windowHeight - 1);
        }
    }
}
