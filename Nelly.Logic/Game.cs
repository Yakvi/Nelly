using System;
using System.Runtime.CompilerServices;
using Nelly.Database;

namespace Nelly.Logic
{
    public static class Game
    {
        public static void GetNextSlide(this GameState gameState, Command cmd)
        {
            Slide slide = FetchSlide(gameState, (int)cmd);

            if (slide != null)
            {
                foreach (var str in slide.Strings)
                {
                    gameState.QueueString(str);
                }

                if (slide.NextSlideIds.Count > 1)
                {
                    gameState.ActionNecessary = true;
                }
                else if (slide.NextSlideIds.Count > 0)
                {
                    gameState.NextSlide = Slides.Get(slide.NextSlideIds[0]);
                }
                else
                {
                    gameState.NextSlide = null;
                }
            }
            else
            {
                gameState.IsRunning = false;
            }
        }

        private static Slide FetchSlide(GameState gameState, int selection)
        {
            var slide = gameState.NextSlide;
            if (gameState.ActionNecessary)
            {
                if (selection <= slide.NextSlideIds.Count)
                {
                    slide = Slides.Get(slide.NextSlideIds[selection]);
                    gameState.ActionNecessary = false;
                }
                else
                {
                    gameState.QueueString("Введено неверное значение");
                }
            }

            return slide;
        }

        public static void GetNextSlide(this GameState gameState)
        {
            gameState.GetNextSlide(Command.Any);
        }

    }
}
