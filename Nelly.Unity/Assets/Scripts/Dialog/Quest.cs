using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Game/Dialog/Quest", order = 0)]
public class Quest : Narrative
{
    public Status QuestStatus;
    public Narrative StartingBranch;
    public Location StartingPosition;

    public Narrative[] BranchTree;

    private Narrative currentBranch;

    public override Slide GetNextSlide()
    {
        return null; //TODO
    }

    public override Narrative GetNextUnit()
    {
        return null; //TODO
    }

    public override void Reset()
    {
        QuestStatus = Status.Inactive;
        currentBranch = StartingBranch;
    }
}
