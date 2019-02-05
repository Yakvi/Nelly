using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    public bool SingleChoice;
    public string DefaultButtonText = "";

    private Image imageOutput;
    private TextMeshProUGUI mainOutput;
    private TextMeshProUGUI botOutput;
    private TMButton[] buttons;
    private InputManager inputManager;

    void Awake()
    {
        imageOutput = GameObject.Find("MainGraphicOutput").GetComponent<Image>();
        mainOutput = GameObject.Find("MainTextOutput").GetComponent<TextMeshProUGUI>();
        botOutput = GameObject.Find("BotTextOutput").GetComponent<TextMeshProUGUI>();
        inputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();

        buttons = new TMButton[(int) Selection.Count];
        for (int i = 0; i < (int) Selection.Count; i++)
        {
            buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        }
    }

    public Selection ProcessInteractions()
    {
        var result = Selection.None;

        for (int i = 0; i < buttons.Length; i++)
        {
            var button = GetButton(i);
            if (button)
            {
                if (button.IsHot(i) || IsSingleChoiceSelected(button))
                {
                    result = (Selection) i;
                    break;
                }
            }
        }

        return result;
    }

    public void ChangeSlide(Slide slideData)
    {
        ClearSlide();

        // Rendering
        if (slideData != null)
        {
            botOutput.text = slideData.DialogText;
            SetPicture(slideData);
            SetButtons(slideData.Choices, slideData.IsLinear());
        }
        else
        {
            botOutput.text = "Slide data is not found. GG.";
            Application.Quit();
        }
    }

    private void SetButtons(Choice[] buttonData, bool slideIsLinear)
    {
        if (slideIsLinear)
        {
            AddSingleChoiceButton();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                // TODO: choice determination logic
                if (i < buttonData.Length && buttonData[i])
                {
                    SetButtonText(buttonData[i].Text, i);
                }
            }
        }
    }

    private void AddSingleChoiceButton()
    {
        SingleChoice = true;
        SetButtonText(DefaultButtonText);
    }

    private TMButton GetButton(int index)
    {
        TMButton result = null;

        if (buttons[index].IsActive)
        {
            result = buttons[index];
        }

        return result;
    }

    private bool IsSingleChoiceSelected(TMButton button)
    {
        var result = SingleChoice &&
            (inputManager.AnyKey || button.WasClicked);
        return result;
    }

    private void SetPicture(Slide slide)
    {
        // TODO: Do we want to preserve previous image or reset to nothing? 
        //       Preserve previous for now, if not, implement in ClearSlide
        imageOutput.sprite = slide.Image ? slide.Image : null;
        if (slide.ImageTint != Color.white) imageOutput.color = slide.ImageTint;
        mainOutput.text = slide.ImageText;
    }

    private void SetButtonText(string text, int pos = 3)
    {
        buttons[pos].SetText(text);
    }

    private void ClearSlide()
    {
        SingleChoice = false;
        foreach (var button in buttons)
        {
            button.SetText("");
        }
    }

}
