using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nelly.Database
{
    public static class Slides
    {
        static Slides()
        {
            slides = new List<Slide>();
            FetchSlides();
        }

        private static void FetchSlides()
        {
            FetchLinearFromFile("cutscene1.txt");
            FetchChoice("choice1.txt");
            FetchLinearFromFile("cutscene2.txt");

        }

        private static void FetchChoice(string fileName)
        {
            var source = FetchStringsFromFile(fileName);

            if (source != null)
            {
                var slide = Create();
                slide.NextSlideIds.Clear();
                slide.Strings.Add(source[0]);
                for (int i = 1; i < source.Length; i++)
                {
                    var line = source[i];
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        slide.Strings.Add($"{i}. {line}");
                        slide.NextSlideIds.Add(slides.Count + i);
                    }
                }
                slides.Add(slide);
            }
        }

        private static string[] FetchStringsFromFile(string fileName)
        {
            string[] result = null;

            if (File.Exists(fileName))
            {
                result = File.ReadAllLines(fileName, Encoding.UTF8);
            }

            return result;
        }

        private static void FetchLinearFromFile(string fileName)
        {
            var source = FetchStringsFromFile(fileName);
            if (source != null)
            {
                var slide = Create();
                for (int i = 0; i < source.Length; i++)
                {
                    var line = source[i];
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        slide.Strings.Add(line);
                        if (line[0] != '*')
                        {
                            slides.Add(slide);
                            slide = Create();
                        }
                    }
                    else if (slide.Strings.Count > 0)
                    {
                        slides.Add(slide);
                        slide = Create();
                    }
                }
            }
        }

        private static Slide Create()
        {
            var slide = new Slide("");
            slide.NextSlideIds.Add(slides.Count + 1); //TODO: actual linking
            return slide;
        }

        public static Slide Get(int index)
        {
            Slide result = null;

            if (index < slides.Count)
            {
                result = slides[index];
            }

            return result;
        }

        public static Slide First()
        {
            return Get(0);
        }

        private static List<Slide> slides { get; set; }
    }
}
