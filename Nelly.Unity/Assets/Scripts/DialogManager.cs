using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public ScriptableObject StartingBranch;
    [SerializeField]
    private INarrative currentBranch;
    private Slide activeSlide;

    private GameManager gameManager;
    private SlideManager slideManager;
    private MapManager mapManager;

    private bool slideChanged;

    void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        slideManager = gameObject.GetComponent<SlideManager>();
    }

    void Start()
    {
        Restart();
    }

    void Update()
    {
        slideChanged = false;

        ProcessMap();
        ProcessUI();

        if (slideChanged)
        {
            slideManager.ChangeSlide(activeSlide);
        }
    }

    public void Restart()
    {
        if (StartingBranch != null)
        {
            var branch = StartingBranch as INarrative;
            StartBranch(branch);
            slideManager.Restart();
            slideManager.ChangeSlide(activeSlide);
        }
    }

    private void ProcessUI()
    {
        if (slideManager.isActiveAndEnabled)
        {
            var index = slideManager.LastInteraction;
            if (index != PlayerChoice.None) // we have a hit
            {
                slideChanged = true;
                if (activeSlide.Choices.Length > (int) index) // We have an option corresponding to the hit
                {
                    // Get slide based on choice
                    var nextBranch = activeSlide.Choices[(int) index]?.Branch;
                    if (nextBranch != null)
                    {
                        StartBranch(nextBranch as INarrative);
                    }
                }
                else
                {
                    // Get next slide in unit
                    activeSlide = GetNextSlideFromUnit();
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
                StartBranch(point.Branch);
            }
        }
    }

    private void StartBranch(INarrative branch)
    {
        branch.Reset();
        currentBranch = branch;
        activeSlide = currentBranch.GetNextSlide();
    }

    private Slide GetNextSlideFromUnit()
    {
        var result = currentBranch.GetNextSlide();

        if (result == null)
        {
            currentBranch = currentBranch.GetNextUnit();
            if (currentBranch != null)
            {
                result = currentBranch.GetNextSlide();
            }
            else
            {
                print("no other units");
            }
        }

        return result;
    }
}
