using System;
using System.Collections.Generic;

namespace Nelly.Database
{
    public class UnitRepository
    {
        private static List<Unit> units { get; set; }
        private static int guid = 0;

        public static Unit Initialize()
        {
            units = new List<Unit>();

            var u0 = AddUnit("data/cutscene1.txt"); // Cutscene and first quest
            var u1 = AddUnit("data/choice1.txt"); // How do you want to get there? 0. Bus 1. Tram
            var u2 = AddUnit("data/walking-meet.txt", u1, 0); // Bus 
            var u3 = AddUnit("data/choice2.txt"); // Give money? 0. Yes 1. No
            var u4 = AddUnit("data/give-money.txt", u3, 0); // Yes, finish tree
            var u5 = AddUnit("data/no-money.txt", u3, 1); // No, finish tree
            var u6 = AddUnit("data/tram-way.txt", u1, 1); // Tram
            var u7 = AddUnit("data/choice3.txt"); // 1. Continue further, 2. Get off later, 3. Get off now

            var end = AddUnit("data/end.txt");
            u4.AddNextUnitId(end.Id, 0);
            u5.AddNextUnitId(end.Id, 0);

            return units[0];
        }

        private static Unit AddUnit(string fileName, Unit parent, int index)
        {
            var result = new Unit(fileName, guid);
            units.Add(result);
            if (parent != null)
            {
                parent.AddNextUnitId(result.Id, index);
            }

            ++guid;

            return result;
        }

        private static Unit AddUnit(string fileName)
        {
            var unit = AddUnit(fileName, null, 0);

            return unit;
        }

        internal static Unit Get(int index)
        {
            Unit result = null;

            if (index <= units.Count - 1)
            {
                result = units[index];
            }

            return result;
        }
    }
}
