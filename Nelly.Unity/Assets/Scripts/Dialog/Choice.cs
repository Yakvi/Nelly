using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Choice", menuName = "Game/Dialog/Choice", order = 0)]
public class Choice : ScriptableObject 
{
    public string Text = "";
    public Narrative Branch;

    public Location[] POI;

    public float CostInTime = 0f;
    public float CostInMoney = 0f;
}
