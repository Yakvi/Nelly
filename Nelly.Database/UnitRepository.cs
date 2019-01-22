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
            var u2 = AddBranch("data/walking-meet.txt", u1, 0); // Bus 
            var u3 = AddUnit("data/choice2.txt"); // Give money? 0. Yes 1. No
            var u4 = AddBranch("data/give-money.txt", u3, 0); // Yes
            var u5 = AddBranch("data/no-money.txt", u3, 1); // No
            var u6 = AddBranch("data/tram-way.txt", u1, 1); // Tram
            var u7 = AddUnit("data/choice3.txt"); // Get off now, continue riding

            return units[0];
        }

        private static Unit AddUnit(string fileName)
        {
            var unit = new Unit(fileName, guid++);
            units.Add(unit);

            return unit;
        }

        private static Unit AddBranch(string fileName, Unit parent, int index)
        {
            var result = new Unit(fileName, guid);
            units.Add(result);
            parent.AddNextUnitId(guid, index);

            ++guid;

            return result;
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
