using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nelly.Database
{
    public class Unit
    {
        public bool ActionNecessary { get; set; }
        public int Id { get; }

        private List<Slide> slides { get; set; }
        private int currentSlideIndex = 0;
        private int[] nextUnitIds { get; set; }

        public Unit(string fileName, int index)
        {
            Id = index;
            slides = new List<Slide>();
            CreateSlides(ReadFile(fileName));
        }

        private void CreateSlides(string[] source)
        {
            if (source != null)
            {
                var firstSlide = new Slide();
                if (source[0][0] == '$')
                {
                    AddChoice(source, firstSlide);
                }
                else
                {
                    AddLinear(source, firstSlide);
                }
            }
        }

        private void AddChoice(string[] source, Slide slide)
        {
            nextUnitIds = new int[source.Length - 1];

            slide.Strings.Add(source[0]);
            for (int i = 1; i < source.Length; i++)
            {
                var line = source[i];
                if (!String.IsNullOrWhiteSpace(line))
                {
                    slide.Strings.Add($"{i}. {line}");
                }
            }

            slides.Add(slide);
        }

        private void AddLinear(string[] source, Slide slide)
        {
            for (int i = 0; i < source.Length; i++)
            {
                var line = source[i];
                if (!String.IsNullOrWhiteSpace(line))
                {
                    slide.Strings.Add(line);
                    if (line[0] != '*')
                    {
                        slides.Add(slide);
                        slide = new Slide();
                    }
                }
                else if (slide.Strings.Count > 0)
                {
                    slides.Add(slide);
                    slide = new Slide();
                }
            }
        }

        private static string[] ReadFile(string fileName)
        {
            string[] source = null;
            if (File.Exists(fileName))
            {
                source = File.ReadAllLines(fileName, Encoding.UTF8);
            }

            return source;
        }

        public Slide GetNextSlide()
        {
            Slide slide = null;
            if (currentSlideIndex != slides.Count)
            {
                slide = slides[currentSlideIndex++];
            }

            return slide;

        }

        public Unit SelectNext(int cmd)
        {
            Unit result = UnitRepository.Get(Id + 1);
            return result;
            // {
            //     slide = Slides.Get(slide.NextSlideIds[selection]);
            //     gameState.ActionNecessary = false;
            // }
            // else
            // {
            //     gameState.QueueString("Введено неверное значение");
            // }
        }
    }
}
