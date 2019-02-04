using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "Dialog/Unit", order = 0)]
public class Unit : ScriptableObject
{
    public Slide[] Slides;
    public Unit NextUnit;
    private int currentSlide;

    public void Reset()
    {
        currentSlide = 0;
    }

    public Slide GetNextSlide()
    {
        Slide result = null;
        if (currentSlide < Slides.Length)
        {
            result = Slides[currentSlide++];
        }

        return result;
    }

    internal Unit GetNextUnit()
    {
        if (NextUnit) NextUnit.Reset();
        
        return NextUnit;
    }
}
