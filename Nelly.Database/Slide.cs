using System.Collections.Generic;

namespace Nelly.Database
{
    public class Slide
    {
        public Slide(string str)
        {
            ID = _idCounter++;
            Strings = new List<string>();
            NextSlideIds = new List<int>();


            Strings.Add(str);   
        }
        public List<string> Strings { get; set; }
        public List<int> NextSlideIds { get; set; }
        public int ID { get; set; }

        private static int _idCounter = 0;
    }
}