using System;
using System.Runtime.CompilerServices;
using Nelly.Logic;

[assembly : InternalsVisibleTo("Nelly_cnsl.Tests")]
namespace Nelly
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            GameState gameState = Start();
            while (gameState.IsRunning)
            {
                Update(gameState);
            }
        }

        private static GameState Start()
        {
            var gameState = new GameState();
            // Welcome message
            Render.Clear();
            Render.String("НА СВОИХ ДВОИХ");
            Render.String("");
            Render.String("");
            Render.String("");
            Render.String("Версия 0.001 (прототип)");
            Render.String("");

            Input.Any();
            Render.Process(gameState.QueuedStrings);

            return gameState;
        }

        internal void assert(bool condition)
        {
            if (!condition)
            {
                throw new InvalidProgramException();
            }
        }

        internal static void Update(GameState gameState)
        {
            if (gameState.ActionNecessary)
            {
                var input = new Input();

                if (input.Cmd == Command.Exit)
                {
                    gameState.IsRunning = false;
                }
                else
                {
                    gameState.GetNextSlide(input.Cmd);
                }
            }
            else
            {
                Input.Any();
                gameState.GetNextSlide();
            }
            Render.Process(gameState.QueuedStrings);

        }

        internal static int Answer()
        {
            return 42;
        }
    }
}
