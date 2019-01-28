using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private readonly int buttonCount = 4;
    public GameObject MainTextArea;
    public Image Image;
    public TMButton[] Buttons;

    // Start is called before the first frame update
    // void Start()
    // {
    //     Buttons = new TMButton[buttonCount];
    // }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            var button = Buttons[i];
            if (button)
            {
                if (String.IsNullOrWhiteSpace(button.Text))
                {
                    button.This.SetActive(false);
                }
                else
                {
                    button.This.SetActive(true);
                }
            }
        }
    }
}
