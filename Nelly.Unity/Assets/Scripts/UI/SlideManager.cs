using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    public string DefaultButtonText = "";
    [Range(0.0f, 1.0f)]
    public float GlobalSoundVolume = 1.0f;
    [Range(0.0f, 1.0f)]
    public float GlobalAmbientVolume = 1.0f;

    public Transform Player;

    public bool ToggleRequested;
    public Selection LastInteraction;

    private Window window;
    private Image imageOutput;
    private TextMeshProUGUI mainOutput;
    private TextMeshProUGUI botOutput;
    private TMButton[] buttons;
    private InputManager inputManager;

    private AudioSource fxSound;
    private AudioSource ambientSound;
    private bool singleChoice;
    private bool soundsOn;

    void Awake()
    {
        window = gameObject.GetComponentInChildren<Window>();
        imageOutput = GameObject.Find("MainGraphicOutput").GetComponent<Image>();
        mainOutput = GameObject.Find("MainTextOutput").GetComponent<TextMeshProUGUI>();
        botOutput = GameObject.Find("BotTextOutput").GetComponent<TextMeshProUGUI>();
        inputManager = FindObjectOfType<InputManager>();

        fxSound = GameObject.Find("FXSound").GetComponent<AudioSource>();
        ambientSound = GameObject.Find("AmbientSound").GetComponent<AudioSource>();

        buttons = new TMButton[(int) Selection.Count];
        for (int i = 0; i < (int) Selection.Count; i++)
        {
            buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        }
    }

    void Update()
    {
        LastInteraction = Selection.None;

        if (ToggleRequested)
        {
            ToggleRequested = false;
            window.Toggle();
        }
        else
        {
            ProcessInteractions();
        }

        if (window.IsOpen != soundsOn)
        {
            soundsOn = window.IsOpen;
            ToggleSounds();
        }
    }

    public void ToggleSounds()
    {
        if (!window.IsOpen)
        {
            fxSound.Stop();
            ambientSound.Stop();
        }
        else
        {
            ambientSound.Play();
        }
    }

    public void ProcessInteractions()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = GetButton(i);
            if (button)
            {
                if (button.IsHot(i) || IsSingleChoiceSelected(button))
                {
                    LastInteraction = (Selection) i;
                    break;
                }
            }
        }
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
            PlaySounds(slideData);
            ChangeLocation(slideData.NewLocation);
        }
        else
        {
            botOutput.text = "Slide data is not found. GG.";
            Application.Quit();
        }
    }

    private void ChangeLocation(Location location)
    {
        if (location != null)
        {
            Player.position = location.Coordinates;
        }
    }

    private void PlaySounds(Slide slideData)
    {
        if (slideData.Sound != null)
        {
            fxSound.clip = slideData.Sound;
            fxSound.volume = GlobalSoundVolume * slideData.SoundVolume;
            fxSound.Play();
        }
        if (slideData.Ambient != null)
        {
            ambientSound.clip = slideData.Ambient;
            ambientSound.volume = GlobalAmbientVolume * slideData.AmbientVolume;
            ambientSound.Play();
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
        singleChoice = true;
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
        var result = singleChoice && button.WasClicked();

        if (window && window.IsOpen) result |= inputManager.AnyKeyUp;

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
        singleChoice = false;
        foreach (var button in buttons)
        {
            button.SetText("");
        }
    }

}
