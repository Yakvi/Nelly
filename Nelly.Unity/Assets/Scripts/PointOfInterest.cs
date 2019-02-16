using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    // params
    public GameObject Tooltip;
    public TextMeshPro TooltipText;
    public SpriteRenderer Icon;
    public Narrative Branch;
    public bool WasSelected = false;

    void Awake()
    {
        Icon = gameObject.GetComponent<SpriteRenderer>();

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
    void OnMouseUp() {
        WasSelected = true;
    }
}
