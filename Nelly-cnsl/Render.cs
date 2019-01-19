using System;
using System.Collections.Generic;
using Nelly.Logic;

namespace Nelly
{
    internal class Render
    {
        static Render()
        {
            if (!IsInitialized)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                IsInitialized = true;
            }
        }

        internal static void String(string str)
        {
            Console.WriteLine(str);
        }

        internal static void Process(List<string> strings)
        {
            Clear();
            String("");
            foreach (var str in strings)
            {
                String(str);
            }
            strings.Clear();
        }

        internal static void Clear()
        {
            Console.Clear();
        }

        public static bool IsInitialized = false;
    }
}
