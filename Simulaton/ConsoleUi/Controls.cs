using Simulaton.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.ConsoleUi
{
    class Controls
    {
        private const int CONTROLS_HEIGHT = 3;
        private const int CONTROLS_SCROLLSPEED = 1;

        private const char CONTROLS_SHOW_ENTITIES = '1';
        private const char CONTROLS_SHOW_LOGS = '2';
        private const char CONTROLS_CONINUE_NEXT_TICK = 'c';
        private const char CONTROLS_STEP_THROUGH_ENTITIES = 'n';
        private const char CONTROLS_SCROLL_UP = 'w';
        private const char CONTROLS_SCROLL_DOWN = 's';

        private int width;
        private int height;
        private char lastShownFrame;
        private int currentScrollOffsetLog;
        private int currentScrollOffsetEntityView;
        private ConsolePresenter presenter;

        public Controls(ConsolePresenter presenter, int width, int height)
        {
            currentScrollOffsetLog = 0;
            currentScrollOffsetEntityView = 0;
            lastShownFrame = '1';
            this.presenter = presenter;
            this.width = width;
            this.height = height;
        }

        internal ConsoleFrame GetControlsFrame()
        {
            ConsoleFrame ui = new ConsoleFrame(width - 1, CONTROLS_HEIGHT);
            int relevantScrollOffset = lastShownFrame == CONTROLS_SHOW_ENTITIES ? currentScrollOffsetEntityView : currentScrollOffsetLog;
            string instructions =
                CONTROLS_SHOW_ENTITIES + ": Show entities,  " +
                CONTROLS_SHOW_LOGS + ": Show Logs,  " +
                CONTROLS_CONINUE_NEXT_TICK + ": Continue, " +
                CONTROLS_SCROLL_UP + "/" +
                CONTROLS_SCROLL_DOWN + ": Scroll(" + relevantScrollOffset + ")";


            ui.CreateBorder();
            ui.InsertEarliest(instructions);
            return ui;
        }

        internal void NextTick(SummaryManager data)
        {
            UpdateUI(data, lastShownFrame);
        }

        public void UserAction(SummaryManager data)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            UpdateUI(data, consoleKeyInfo.KeyChar);
        }

        public void UpdateUI(SummaryManager data, char key)
        {
            switch (key)
            {
                case CONTROLS_SHOW_ENTITIES:
                    lastShownFrame = key;
                    presenter.RenderCurrentTick(data, currentScrollOffsetEntityView);
                    break;
                case CONTROLS_SHOW_LOGS:
                    lastShownFrame = key;
                    presenter.ShowLog(data, currentScrollOffsetLog);
                    break;
                case CONTROLS_SCROLL_UP:
                    Scroll(data, lastShownFrame, -CONTROLS_SCROLLSPEED);
                    break;
                case CONTROLS_SCROLL_DOWN:
                    Scroll(data, lastShownFrame, CONTROLS_SCROLLSPEED);
                    break;
                case CONTROLS_CONINUE_NEXT_TICK:
                    // Next Tick
                    break;
                default:
                    UserAction(data);
                    break;
            }
        }

        private void Scroll(SummaryManager data, char lastShownFrame, int scrollSpeed)
        {
            if (lastShownFrame == CONTROLS_SHOW_ENTITIES)
            {
                currentScrollOffsetEntityView += scrollSpeed;
                if (currentScrollOffsetEntityView < 0) currentScrollOffsetEntityView = 0;
                presenter.RenderCurrentTick(data, currentScrollOffsetEntityView);
            }
            else if (lastShownFrame == CONTROLS_SHOW_LOGS)
            {
                currentScrollOffsetLog += scrollSpeed;
                if (currentScrollOffsetLog < 0) currentScrollOffsetLog = 0;
                presenter.ShowLog(data, currentScrollOffsetLog);
            }
        }
    }
}
