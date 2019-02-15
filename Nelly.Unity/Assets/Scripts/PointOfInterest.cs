using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public Location Location;

    private SpriteRenderer spriteRenderer;
    
    public GameObject Tooltip;
    public TextMeshPro TooltipText;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Tooltip.SetActive(false);
    }

    void OnMouseEnter()
    {
        Tooltip.SetActive(true);
    }
    void OnMouseExit()
    {
        Tooltip.SetActive(false);
    }

}
