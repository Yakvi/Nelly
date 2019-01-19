using Nelly.Database;
using System;
using System.Collections.Generic;

namespace Nelly.Logic
{
    public class GameState
    {
        public GameState()
        {
            IsRunning = true;
            ActionNecessary = false;
            NextSlide = Slides.First();

            QueueString("Добро пожаловать!");
            QueueString("Эта версия игры выполнена в командной строке.");
            QueueString("Для выхода из игры, введите 'exit' или 'выход'.");
        }

        internal void QueueString(string str)
        {
            QueuedStrings.Add(str);
        }

        public List<string> QueuedStrings = new List<string>();
        public bool IsRunning { get; set; }
        public bool ActionNecessary { get; set; }
        public Slide NextSlide { get; set; }

    }
}