using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject Player;
    public PointOfInterest DefaultPOI;

    public void SetPosition(GameObject POI, Vector2 position)
    {
        POI.transform.position = position;
    }
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
            SetPosition(Player, location.Position);
        }
    }
}
