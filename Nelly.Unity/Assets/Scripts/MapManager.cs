using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform Map;
    public Transform Player;
    public PointOfInterest DefaultPOI;

    public void ClearTempPOI()
    {
        // TODO clear list of temp points
    }
    public void SetPOI(Choice choice)
    {
        foreach (var point in choice.POI)
        {

            //TODO instantiate points
            //TODO if temporary store them in a list
        }
    }
    public void ChangePlayerPosition(Location location)
    {
        if (location != null)
        {
            Player.position = location.Position;
        }
    }
}
