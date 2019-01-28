using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject MainTextArea;
    public Image Image;
    public TMButton[] Buttons;

    private InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();
    }

    void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            var button = Buttons[i];
            if (button)
            {
                var text = (inputManager.Actions[i] || button.IsClicked) ? "Button Pressed" : i.ToString();
                button.SetText(text);
            }
        }
    }
}
