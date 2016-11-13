﻿using Simulaton.Mechanics;
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

        private const char CONTROLS_SHOW_ENTITIES = '1';
        private const char CONTROLS_SHOW_LOGS = '2';
        private const char CONTROLS_CONINUE_NEXT_TICK = 'c';
        private const char CONTROLS_SCROLL_UP = 'w';
        private const char CONTROLS_SCROLL_DOWN = 's';

        private int width;
        private int height;
        private char lastShownFrame;
        private int currentScrollOffset;
        private ConsolePresenter presenter;

        public Controls(ConsolePresenter presenter, int width, int height)
        {
            currentScrollOffset = 0;
            lastShownFrame = '1';
            this.presenter = presenter;
            this.width = width;
            this.height = height;
        }

        internal ConsoleFrame GetControlsFrame(bool scrollable)
        {
            ConsoleFrame ui = new ConsoleFrame(width - 1, CONTROLS_HEIGHT);
            string instructions = 
                CONTROLS_SHOW_ENTITIES + ": Show entities,  " + 
                CONTROLS_SHOW_LOGS + ": Show Logs,  " + 
                CONTROLS_CONINUE_NEXT_TICK + ": Continue";
            if (scrollable)
            {
                instructions += ",  " +
                    CONTROLS_SCROLL_UP + "/" +
                    CONTROLS_SCROLL_DOWN + ": Scroll";
            }

            ui.CreateBorder();
            ui.InsertEarliestTopLeft(instructions);
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
                    presenter.RenderCurrentTick(data);
                    break;
                case CONTROLS_SHOW_LOGS:
                    lastShownFrame = key;
                    presenter.ShowLog(data, currentScrollOffset);
                    break;
                case CONTROLS_SCROLL_UP:
                    currentScrollOffset += 2;
                    presenter.ShowLog(data, currentScrollOffset);
                    break;
                case CONTROLS_SCROLL_DOWN:
                    if (currentScrollOffset > 0) currentScrollOffset -= 2;
                    presenter.ShowLog(data, currentScrollOffset);
                    break;
                case CONTROLS_CONINUE_NEXT_TICK:
                    // Next Tick
                    currentScrollOffset = 0;
                    break;
                default:
                    UserAction(data);
                    break;
            }
        }

    }
}
