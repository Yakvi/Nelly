using System.Collections.Generic;

namespace Nelly.Database
{
    public class Slide
    {
        public Slide()
        {
            ID = _idCounter++;
            Strings = new List<string>();
            NextSlideIds = new List<int>();


            Strings.Add("");   
        }
        public List<string> Strings { get; set; }
        public List<int> NextSlideIds { get; set; }
        public int ID { get; set; }

        private static int _idCounter = 0;
    }
}