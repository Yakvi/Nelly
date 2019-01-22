using System;
using System.Runtime.CompilerServices;
using Nelly.Database;

namespace Nelly.Logic
{
    public static class Game
    {
        public static void GetNextSlide(this GameState gameState, Command cmd)
        {
            var unit = gameState.CurrentUnit;

            if (unit != null)
            {
                Slide slide = unit.GetNextSlide();
                if (slide != null)
                {
                    foreach (var str in slide.Strings)
                    {
                        gameState.QueueString(str);
                    }
                }
                else
                {
                    gameState.CurrentUnit = unit.SelectNext((int) cmd);
                }

                gameState.ActionNecessary = unit.ActionNecessary;
            }
            else
            {
                gameState.IsRunning = false;
            }
        }

        public static void GetNextSlide(this GameState gameState)
        {
            gameState.GetNextSlide(Command.Any);
        }

    }
}
