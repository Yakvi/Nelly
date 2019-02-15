using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Location", menuName = "Game/Location", order = 0)]
public class Location : ScriptableObject
{
    public string Name = "";
    public Vector2 Coordinates;
    public PointOfInterest POI;
    public Sprite Icon;
}
