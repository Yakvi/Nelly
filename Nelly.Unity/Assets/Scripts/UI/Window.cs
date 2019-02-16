using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool KeyUpCaptured;
    private InputManager inputManager;

    private TMButton closeButton;


    public virtual void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        closeButton = transform.Find("Close Button").GetComponent<TMButton>();
    }

    public virtual void Update()
    {
        KeyUpCaptured = inputManager.AnyKeyUp;
    }

    public bool IsOpen()
    {
        if (closeButton && closeButton.WasClicked())
        {
            Toggle();
        }
        return gameObject.activeSelf;
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
