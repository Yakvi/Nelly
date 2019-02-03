using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    public int buttonCount;
    public Slide CurrentSlide;

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

        buttons = new TMButton[buttonCount];
        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        }
    }

    void Start()
    {
        ChangeSlide(CurrentSlide.Choices);
    }

    public int ProcessInteractions()
    {
        var result = -1;

        for (int i = 0; i < buttons.Length; i++)
        {
            var button = GetButton(i);
            if (button)
            {
                if (button.IsHot(i) || IsSingleChoiceSelected(button))
                {
                    result = i;
                    break;
                }
            }
        }

        return result;
    }

    public void ChangeSlide(Choice[] choices = null)
    {
        ClearSlide();

        // Rendering
        botOutput.text = CurrentSlide.DialogText;
        SetPicture(CurrentSlide);

        if (choices == null || choices.Length < 2)
        {
            AddSingleChoiceButton();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                // TODO: choice select (in slide)
                if (i < choices.Length && choices[i])
                {
                    SetButtonText(choices[i].Text, i);
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
        mainOutput.text = CurrentSlide.ImageText;
    }

    private void SetButtonText(string text, int pos = 0)
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
