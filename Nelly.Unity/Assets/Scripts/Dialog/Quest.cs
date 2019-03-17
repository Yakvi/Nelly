using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Game/Dialog/Quest", order = 0)]
public class Quest : Location, INarrative
{
    public Status QuestStatus;
    public INarrative StartingBranch;

    public INarrative[] BranchTree;
    private INarrative currentBranch;

    public Slide GetNextSlide()
    {
        throw new NotImplementedException();
    }

    public INarrative GetNextUnit()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        QuestStatus = Status.Inactive;
        currentBranch = StartingBranch;
    }
}
