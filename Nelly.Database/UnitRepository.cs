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

            units.Add(new Unit("cutscene1.txt", guid++));
            units.Add(new Unit("choice1.txt", guid++));
            units.Add(new Unit("cutscene2.txt", guid++));

            return units[0];
        }

        internal static int Add(Unit unit)
        {
            units.Add(unit);
            ++guid;
            
            return guid;
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
