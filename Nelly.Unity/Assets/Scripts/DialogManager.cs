using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public int buttonCount;
    public Slide CurrentSlide;
    public string DefaultButtonText = "";

    private Image imageOutput;
    private TextMeshProUGUI dialogOutput;
    private TMButton[] buttons;

    void Awake()
    {
        imageOutput = GameObject.Find("MainGraphicOutput").GetComponent<Image>();
        dialogOutput = GameObject.Find("MainTextOutput").GetComponent<TextMeshProUGUI>();

        buttons = new TMButton[buttonCount];
        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        }
    }

    void Update()
    {
        if (CurrentSlide)
        {
            dialogOutput.text = CurrentSlide.DialogText;
            SetPicture(CurrentSlide);
            DrawButtons(CurrentSlide.Choices);
            //ProcessInteractions(CurrentSlide.Choices);
        }
    }

    private void ProcessInteractions(Choice[] choices)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            if (button.IsHot(i))
            {
                // TODO: CurrentSlide = Choice.Slide?
            };
        }
    }

    private void SetPicture(Slide slide)
    {
        // TODO: Do we want to preserve previous image or reset to nothing?
        imageOutput.sprite = slide.Image ? slide.Image : null;
        if (slide.ImageTint != Color.white) imageOutput.color = slide.ImageTint;
        // TODO: imageOutput.text = slide.ImageText;
    }

    private void DrawButtons(Choice[] choices)
    {
        Assert.IsNotNull(buttons);
        Assert.IsTrue(buttonCount > 0);
        Assert.IsTrue(choices.Length > 0);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i >= choices.Length)
            {
                buttons[i].SetText("");
            }
            else
            {
                buttons[i].SetText(choices[i]?.Text);
            }
        }
    }

}
