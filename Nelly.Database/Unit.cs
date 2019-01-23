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
        private int[] nextUnitIds { get; set; }
        private int currentSlideIndex = 0;

        public Unit(string fileName, int index)
        {
            Id = index;
            slides = new List<Slide>();
            nextUnitIds = new int[3];
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
            ActionNecessary = true;

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

        private string[] ReadFile(string fileName)
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

        public void SetNextUnitId(int id, int index)
        {
            nextUnitIds[index] = id;
        }

        public void AddNext(int id)
        {
            SetNextUnitId(id, 0);
        }

        public Unit GetNextUnit(int cmd)
        {
            Reset();

            var nextId = Id + 1;
            if (ActionNecessary)
            {
                if (cmd < nextUnitIds.Length && nextUnitIds[cmd] != 0)
                {
                    nextId = nextUnitIds[cmd];
                    //ActionNecessary = false;
                }
                else
                {
                    nextId = Id;
                    currentSlideIndex = 0;
                }
            }
            else if (nextUnitIds[0] != 0)
            {
                nextId = nextUnitIds[0];
            }

            Unit result = UnitRepository.Get(nextId);

            return result;
        }

        public void Reset()
        {
            currentSlideIndex = 0;
        }
    }
}
