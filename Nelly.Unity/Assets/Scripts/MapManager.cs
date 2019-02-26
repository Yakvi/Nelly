using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform Map;
    public Transform Player;
    
    [SerializeField]
    private PointOfInterest defaultPOI;
    [SerializeField]
    private Sprite defaultIcon;
    
    public List<PointOfInterest> ActivePOI;
    private List<GameObject> tempPOIs;

    private void Awake()
    {
        ActivePOI = new List<PointOfInterest>();
        tempPOIs = new List<GameObject>();
    }

    public void ClearTempPOI()
    {
        foreach (var point in tempPOIs)
        {
            Destroy(point);
        }
        tempPOIs.Clear();
    }
    public void SetPOI(Choice choice)
    {
        var source = choice.POI;

        var POI = Instantiate(defaultPOI, Map);
        POI.transform.position = source.Position != null ? source.Position : new Vector2(0, 0);
        POI.Icon.sprite = source.Icon ? source.Icon : defaultIcon;
        POI.TooltipText.text = POI.gameObject.name = source.Title;

        POI.Branch = choice.Branch;
        ActivePOI.Add(POI);

        if (source.IsTemporary)
        {
            tempPOIs.Add(POI.gameObject);
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
