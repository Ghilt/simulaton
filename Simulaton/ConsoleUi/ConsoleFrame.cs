﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.ConsoleUi
{
    class ConsoleFrame
    {
        private const int POS_OCCUPIED = -2;
        private const int POS_FREE = -1;
        //positive ints are subframe ids

        public char[,] frame { private set; get; }
        private int[,] occupied;
        private int width;
        private int height;

        private List<SubFrameParams> subFrames;

        public ConsoleFrame(int width, int height)
        {
            frame = new char[width, height];
            occupied = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    occupied[x, y] = POS_FREE;
                    frame[x, y] = ' ';
                }
            }

            subFrames = new List<SubFrameParams>();
            this.width = width;
            this.height = height;
        }

        public void CreateBorder()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool left = x == 0;
                    bool right = x == width - 1;
                    bool top = y == 0;
                    bool bot = y == height - 1;

                    if (top && right)
                    {
                        Insert(x, y, '┐');
                    }
                    else if (top && left)
                    {
                        Insert(x, y, '┌');
                    }
                    else if (bot && left)
                    {
                        Insert(x, y, '└');
                    }
                    else if (bot && right)
                    {
                        Insert(x, y, '┘');
                    }
                    else if (bot || top)
                    {
                        Insert(x, y, '─');
                    }
                    else if (left || right)
                    {
                        Insert(x, y, '│');
                    }
                }
            }
        }

        public string GetFrameRender()
        {
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (occupied[x, y] >= 0) // Get data from subframe
                    {
                        int subFrameId = occupied[x, y];
                        char subFrameChar = subFrames[subFrameId].GetCharAt(x, y);
                        builder.Append(subFrameChar);
                    }
                    else
                    {
                        builder.Append(frame[x, y]);
                    }
                    if (x == width - 1)
                    {
                        builder.Append("\n");
                    }
                }
            }
            return builder.ToString();
        }

        public bool Insert(int x, int y, char c)
        {
            if ( c == '\n' || c == '\t')
            {
                throw new ArgumentException();
            }
            if (x >= width || y >= height)
            {
                return false;
            }
            if (occupied[x, y] != POS_FREE)
            {
                return false;
            }
            else
            {
                frame[x, y] = c;
                occupied[x, y] = POS_OCCUPIED;
                return true;
            }
        }

        public bool Insert(int x, int y, string characters)
        {
            string s = ReplaceIllegalCharacters(characters);

            if (!CheckFree(x, y, s.Length, 1))
            {
                return false;
            }
            char[] array = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                Insert(x + i, y, array[i]);
            }
            return true;
        }

        private string ReplaceIllegalCharacters(string s)
        {
            return s.Replace("\t", "    ");
        }

        public bool Insert(int x, int y, ConsoleFrame frame)
        {
            if (!CheckFree(x, y, frame.width, frame.height))
            {
                return false;
            }
            Occupy(x, y, frame);
            return true;
        }

        public bool InsertEarliest(string s)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (Insert(x, y, s))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool InsertEarliestAlongColumn(string s, int x)
        {
            for (int y = 0; y < height; y++)
            {
                if (Insert(x, y, s))
                {
                    return true;
                }
            }
            return false;
        }

        public bool InsertEarliestAlongRow(string s, int y)
        {
            for (int x = 0; x < width; x++)
            {
                if (Insert(x, y, s))
                {
                    return true;
                }
            }
            return false;
        }

        public bool InsertEarliest(ConsoleFrame frame)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (Insert(x, y, frame))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void InsertIgnoreOccupied(int x, int y, string insert)
        {
            if (x + insert.Length > this.width)
            {
                throw new NotImplementedException();
            }
            char[] sArray = insert.ToArray();
            for (int pos = 0; pos < insert.Length; pos++)
            {
                frame[x + pos, y] = sArray[pos];
                occupied[x + pos, y] = POS_OCCUPIED;
            }
        }

        private bool CheckFree(int x, int y, int width, int height)
        {
            if (x + width > this.width || y + height > this.height)
            {
                return false;
            }

            for (int xOfFrame = 0; xOfFrame < width; xOfFrame++)
            {
                for (int yOfFrame = 0; yOfFrame < height; yOfFrame++)
                {
                    if (occupied[x + xOfFrame, y + yOfFrame] != POS_FREE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Occupy(int x, int y, ConsoleFrame frame)
        {
            subFrames.Add(new SubFrameParams(x, y, frame));
            for (int xOfFrame = 0; xOfFrame < frame.width; xOfFrame++)
            {
                for (int yOfFrame = 0; yOfFrame < frame.height; yOfFrame++)
                {
                    occupied[x + xOfFrame, y + yOfFrame] = subFrames.Count - 1;
                }
            }
            return true;
        }
    }
}
