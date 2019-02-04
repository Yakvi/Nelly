using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit CurrentUnit;

    private SlideManager slideManager;
    private Slide currentSlide;

    void Awake()
    {
        CurrentUnit.Reset();

        slideManager = gameObject.GetComponent<SlideManager>();
    }

    void Start()
    {
        currentSlide = CurrentUnit.GetNextSlide(); // Assume starting unit has always at least one slide!
        slideManager.ChangeSlide(currentSlide);
    }
    
    void Update()
    {
        var index = slideManager.ProcessInteractions();
        if (index >= 0)
        {
            // Get next slide/unit
            if (currentSlide.IsLinear())
            {
                currentSlide = GetNextSlideFromUnit();
            }
            else
            {
                currentSlide = null; // choices[index]
                //slideManager.ChangeSlide(); // ??? 
                // TODO GetSlideFromChoice? 
            }

            // Render slide and buttons
            slideManager.ChangeSlide(currentSlide);
        }
    }

    private Slide GetNextSlideFromUnit()
    {
        var result = CurrentUnit.GetNextSlide();

        if (!result)
        {
            CurrentUnit = CurrentUnit.GetNextUnit();
            if (CurrentUnit)
            {
                result = CurrentUnit.GetNextSlide();
            }
            else
            {
                print("no other units");
            }
        }

        return result;
    }
}
