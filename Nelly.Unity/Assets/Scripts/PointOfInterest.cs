using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public Location Location;

    private SpriteRenderer spriteRenderer;
    private GameObject textBox;
    private TextMeshProUGUI text;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        textBox = transform.Find("Text").gameObject;
        text = textBox.GetComponent<TextMeshProUGUI>();
    }

    void OnMouseEnter()
    {
        textBox.SetActive(true);
    }
    void OnMouseExit()
    {
        textBox.SetActive(false);
    }

}
