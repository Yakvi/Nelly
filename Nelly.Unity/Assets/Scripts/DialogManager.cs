using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Slide ActiveSlide;
    public Narrative CurrentBranch;
    
    private GameManager gameManager;
    private SlideManager slideManager;
    private MapManager mapManager;
    
    private bool slideChanged;

    void Awake()
    {
        CurrentBranch.Reset();

        gameManager = gameObject.GetComponent<GameManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        slideManager = gameObject.GetComponent<SlideManager>();
    }

    void Start()
    {
        ActiveSlide = CurrentBranch.GetNextSlide();
        slideManager.ChangeSlide(ActiveSlide);
    }

    void Update()
    {
        slideChanged = false;

        ProcessMap();
        ProcessUI();

        if (slideChanged)
        {
            slideManager.ChangeSlide(ActiveSlide);
        }
    }

    private void ProcessUI()
    {
        if (slideManager.isActiveAndEnabled)
        {
            var index = slideManager.LastInteraction;
            if (index != Selection.None) // we have a hit
            {
                slideChanged = true;
                if (ActiveSlide.Choices.Length > (int) index) // We have an option corresponding to the hit
                {
                    // Get slide based on choice
                    var nextBranch = ActiveSlide.Choices[(int) index]?.Branch;
                    if (nextBranch)
                    {
                        GetBranch(nextBranch);
                    }
                }
                else
                {
                    // Get next slide in unit
                    ActiveSlide = GetNextSlideFromUnit();
                }
            }
        }
    }

    private void ProcessMap()
    {
        foreach (var point in mapManager.ActivePOI)
        {
            if (point.WasSelected)
            {
                point.WasSelected = false;
                slideChanged = true;
                GetBranch(point.Branch);
            }
        }
    }

    private void GetBranch(Narrative branch)
    {
        CurrentBranch = branch;
        CurrentBranch.Reset();
        ActiveSlide = CurrentBranch.GetNextSlide();
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
