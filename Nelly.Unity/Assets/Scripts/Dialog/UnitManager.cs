using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Slide ActiveSlide;
    public Narrative CurrentBranch;
    private SlideManager slideManager;

    void Awake()
    {
        CurrentBranch.Reset();

        slideManager = gameObject.GetComponent<SlideManager>();
    }

    void Start()
    {
        ActiveSlide = CurrentBranch.GetNextSlide();
        slideManager.ChangeSlide(ActiveSlide);
    }

    void Update()
    {
        var index = slideManager.ProcessInteractions();
        if (index != Selection.None) // we have a hit
        {
            if (ActiveSlide.Choices.Length > (int) index)
            {
                // Get slide based on choice
                var nextBranch = ActiveSlide.Choices[(int) index]?.Branch;
                if (nextBranch)
                {
                    CurrentBranch = nextBranch;
                    CurrentBranch.Reset();
                    ActiveSlide = CurrentBranch.GetNextSlide();
                }
            }
            else
            {
                // Get next slide in unit
                ActiveSlide = GetNextSlideFromUnit();
            }

            slideManager.ChangeSlide(ActiveSlide);
        }
    }

    private Slide GetNextSlideFromUnit()
    {
        var result = CurrentBranch.GetNextSlide();

        if (!result)
        {
            CurrentBranch = CurrentBranch.GetNextUnit();
            if (CurrentBranch != null)
            {
                result = CurrentBranch.GetNextSlide();
            }
            else
            {
                print("no other units");
            }
        }

        return result;
    }
}
