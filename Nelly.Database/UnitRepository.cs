using System;
using System.Collections.Generic;

namespace Nelly.Database
{
    public class UnitRepository
    {
        private static List<Unit> units { get; set; }
        private static int guid = 0;
        private static bool isInitialized = false;

        public static Unit Initialize()
        {
            if (!isInitialized)
            {
                units = new List<Unit>();

                var u0 = AddUnit("data/cutscene1.txt"); // Cutscene and first quest
                var u1 = AddUnit("data/choice1.txt"); // How do you want to get there? 0. Bus 1. Tram
                var u2 = AddUnit("data/walking-meet.txt", u1, 0); // Bus 
                var u3 = AddUnit("data/choice2.txt"); // Give money? 0. Yes 1. No
                var u4 = AddUnit("data/give-money.txt", u3, 0); // Yes, and you arrive in time
                var u5 = AddUnit("data/no-money.txt", u3, 1); // No, and you arrive too late
                var u6 = AddUnit("data/tram-way.txt", u1, 1); // Tram
                var u7 = AddUnit("data/choice3.txt"); // 1. Continue further, 2. Get off later, 3. Get off now
                var u8 = AddUnit("data/ride-far.txt", u7, 0); // Continue further. Hear about food
                var u9 = AddUnit("data/ride-mid.txt", u7, 1); // Get off mid-way. Walk the rest
                var u10 = AddUnit("data/get-off-now.txt", u7, 2); // Get off right away
                var u11 = AddUnit("data/choice4.txt", u8, 0); // U hungry?
                var u12 = AddUnit("data/choice5.txt", u11, 0); // What do you want to order?
                var u13 = AddUnit("data/not-hungry.txt", u11, 1); // Let's move on, get it in time.
                var u14 = AddUnit("data/just-in-time-finale.txt"); // You made it to your destination in time. GG.
                var u15 = AddUnit("data/hotdog.txt", u12, 0);
                var u16 = AddUnit("data/burger.txt", u12, 1);
                var u17 = AddUnit("data/sandwich.txt", u12, 2);
                var u18 = AddUnit("data/too-late-finale.txt"); // You didn't make it in time. GG anyway. 

                // NOTE: Got in timef
                u4.SetNextUnitId(u14.Id, 0); // Gave a dollar to the thugs
                u13.SetNextUnitId(u14.Id, 0); // Didn't get food.
                u9.SetNextUnitId(u14.Id, 0); // Got off the tram mid-way, walked straight to the destination.
                // NOTE: Got too late
                u5.SetNextUnitId(u18.Id, 0); // Got mugged by the thugs
                u15.SetNextUnitId(u18.Id, 0); // Ordered hotdog
                u16.SetNextUnitId(u18.Id, 0); // Ordered burger
                u17.SetNextUnitId(u18.Id, 0); // Ordered sandwich
                u10.SetNextUnitId(u18.Id, 0); // Got off too far away

                var end = AddUnit("data/end.txt");
                // Add finales to the End
                u14.SetNextUnitId(end.Id, 0);
                u18.SetNextUnitId(end.Id, 0);
            }

            isInitialized = true;

            return units[0];
        }

        private static Unit AddUnit(string fileName, Unit parent, int index)
        {
            var result = new Unit(fileName, guid);
            units.Add(result);
            if (parent != null)
            {
                parent.SetNextUnitId(result.Id, index);
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
