using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Choice", menuName = "Dialog/Choice", order = 0)]
public class Choice : ScriptableObject 
{
    public string Text = "";
    public Narrative Branch;
    public float CostInTime = 0f;
    public float CostInMoney = 0f;
}
