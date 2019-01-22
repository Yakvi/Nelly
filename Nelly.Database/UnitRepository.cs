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

            // Cutscene and first quest
            units.Add(new Unit("cutscene1.txt", guid++));

            // How do you want to get there? 
            // 0. Bus
            // 1. Tram
            var choice1 = new Unit("choice1.txt", guid++);
            units.Add(choice1);

            // Bus 
            units.Add(new Unit("cutscene2.txt", guid));
            choice1.AddNextUnitId(guid, 0);
            ++guid;

            return units[0];
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
