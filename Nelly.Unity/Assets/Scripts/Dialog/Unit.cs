using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "Game/Dialog/Unit", order = 0)]
public class Unit : ScriptableObject, INarrative
{
    public Slide[] Slides;
    public ScriptableObject NextUnit;
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
        else
        {

        }

        return result;
    }

    public INarrative GetNextUnit()
    {
        INarrative next = null;

        if (NextUnit != null)
        {
            next = NextUnit as INarrative;
            next.Reset();
        }

        return next;
    }
}
