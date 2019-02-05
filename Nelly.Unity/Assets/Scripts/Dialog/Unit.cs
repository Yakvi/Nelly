﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "Dialog/Unit", order = 0)]
public class Unit : Narrative
{
    public Slide[] Slides;
    public Unit NextUnit;
    private int currentSlide;

    public override void Reset()
    {
        currentSlide = 0;
    }

    public override Slide GetNextSlide()
    {
        Slide result = null;
        if (currentSlide < Slides.Length)
        {
            result = Slides[currentSlide++];
        } else
        {
            
        }

        return result;
    }

    public override Unit GetNextUnit()
    {
            if (NextUnit) NextUnit.Reset();

            return NextUnit;
    }
}