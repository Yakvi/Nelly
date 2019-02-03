using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit CurrentUnit;

    private SlideManager slideManager;
    private Slide nextSlide;

    void Awake()
    {
        CurrentUnit.Reset();

        slideManager = gameObject.GetComponent<SlideManager>();
        slideManager.CurrentSlide = CurrentUnit.GetNextSlide();
        GetNextSlideFromUnit();
    }

    private void GetNextSlideFromUnit()
    {
        nextSlide = CurrentUnit.GetNextSlide();

        if (!nextSlide)
        {
            CurrentUnit = CurrentUnit.GetNextUnit();
            if (CurrentUnit)
            {
                nextSlide = CurrentUnit.GetNextSlide();
            }
            else
            {
                print("no other units");
                Application.Quit();
            }
        }
    }

    void Update()
    {
        var index = slideManager.ProcessInteractions();
        if (index >= 0)
        {
            if (slideManager.CurrentSlide.Choices.Length < 2)
            {
                slideManager.CurrentSlide = nextSlide;
                slideManager.ChangeSlide();
                GetNextSlideFromUnit();
            }
            else
            {
                slideManager.CurrentSlide = null; // choices[index]
                slideManager.ChangeSlide(); // ??? 
                // TODO GetSlideFromChoice? 
            }
        }
    }
}
