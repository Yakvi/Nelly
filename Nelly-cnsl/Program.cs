using System;
using System.Runtime.CompilerServices;
using Nelly.Logic;

[assembly : InternalsVisibleTo("Nelly_cnsl.Tests")]
namespace Nelly
{

    internal class Program
    {
        private static Input input { get; set; }
        internal static void Main(string[] args)
        {
            input = new Input();
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
            Render.String("Версия 0.0.0.2 (прототип)");
            Render.String("");

            input.Any("Нажмите любую клавишу, чтобы продолжить.");
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
            // Get input
            string message = gameState.ActionNecessary ? "Ваш выбор?" : "Далее...";

            if (input.Cmd == Command.Exit)
            {
                gameState.IsRunning = false;
            }
            else
            {
                gameState.GetNextSlide(input.Cmd);
            }

            if (gameState.QueuedStrings.Count > 0)
            {
                Render.Process(gameState.QueuedStrings);
                input.Any(message);
            }

        }

        internal static int Answer()
        {
            return 42;
        }
    }
}
