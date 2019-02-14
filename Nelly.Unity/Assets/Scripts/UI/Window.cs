using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool IsOpen;
    private TMButton closeButton;

    public virtual void Awake()
    {
        closeButton = GameObject.Find("Close Button").GetComponent<TMButton>();
        IsOpen = true;
    }

    public virtual void Update()
    {
        if (closeButton && closeButton.WasClicked())
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        gameObject.SetActive(IsOpen);
    }

}
