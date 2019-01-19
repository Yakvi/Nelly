using System;
using Nelly.Logic;

namespace Nelly
{
    internal class Input
    {
        internal Input()
        {
            Value = Console.ReadLine();
            switch (Value.ToLower())
            {
                case "1":
                    Cmd = Command.First;
                    break;
                case "2":
                    Cmd = Command.Second;
                    break;
                case "3":
                    Cmd = Command.Third;
                    break;
                case "4":
                    Cmd = Command.Fourth;
                    break;
                case "выход":
                case "exit":
                    Cmd = Command.Exit;
                    break;
                case "старт":
                case "начать":
                case "start":
                    Cmd = Command.Start;
                    break;
                case "заново":
                case "restart":
                    Cmd = Command.Restart;
                    break;

                default:
                    Cmd = Command.Any;
                    break;
            }
        }
        internal static void Any()
        {
            Render.String("");
            Render.String("Далее ...");
            Console.ReadKey(true);
        }

        public Command Cmd { get; set; }
        public string Value { get; set; }
    }
}
