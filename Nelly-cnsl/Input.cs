using System;
using Nelly.Logic;

namespace Nelly
{
    internal class Input
    {
        internal Input()
        {
            Cmd = Command.Any;
        }

        internal void Read(char Value)
        {
            switch (Value)
            {
                case '1':
                    Cmd = Command.First;
                    break;
                case '2':
                    Cmd = Command.Second;
                    break;
                case '3':
                    Cmd = Command.Third;
                    break;
                case '4':
                    Cmd = Command.Fourth;
                    break;
                case 'в': // RU
                case 'В':
                case 'e': // EN
                case 'E':
                    Cmd = Command.Exit;
                    break;
                case 'с': // RU
                case 'С': 
                case 's': // EN
                case 'S':
                    Cmd = Command.Start;
                    break;
                case 'з': // RU
                case 'З':
                case 'r': // EN
                case 'R':
                    Cmd = Command.Restart;
                    break;

                default:
                    Cmd = Command.Any;
                    break;
            }
        }

        internal void Any(string message)
        {
            Render.String("");
            Render.String(message);
            var key = Console.ReadKey(true);
            Read(key.KeyChar);
        }

        public Command Cmd { get; set; }
        public string Value { get; set; }
    }
}
