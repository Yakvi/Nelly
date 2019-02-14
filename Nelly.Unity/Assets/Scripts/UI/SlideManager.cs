using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    private DialogWindow window;
    private InputManager inputManager;

    private AudioSource fxSound;
    private AudioSource ambientSound;
    private bool singleChoice;
    private bool soundsOn;

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();

        window = gameObject.GetComponentInChildren<DialogWindow>();

        fxSound = GameObject.Find("FXSound").GetComponent<AudioSource>();
        ambientSound = GameObject.Find("AmbientSound").GetComponent<AudioSource>();

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
        for (int i = 0; i < window.ButtonCount; i++)
        {
            var button = window.GetButton(i);
            if (button &&
                (button.IsHot(i) || IsSingleChoiceSelected(button)))
            {
                LastInteraction = (Selection) i;
                break;
            }
        }
    }

    public void ChangeSlide(Slide slideData)
    {
        singleChoice = false;
        window.Clear();

        // Rendering
        if (slideData != null)
        {
            window.SetTitle(slideData.ImageText);
            window.SetSubtitle(slideData.DialogText);
            window.SetPicture(slideData.Image, slideData.ImageTint);
            SetButtons(slideData.Choices, slideData.IsLinear());

            PlaySounds(slideData);
            ChangeLocation(slideData.NewLocation);
        }
        else
        {
            window.SetSubtitle("Slide data is not found. GG.");
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
            for (int i = 0; i < window.ButtonCount; i++)
            {
                // TODO: Actual choice selection? 

                if (i < buttonData.Length && buttonData[i])
                {
                    window.SetButtonText(buttonData[i].Text, i);
                }
            }
        }
    }

    private void AddSingleChoiceButton()
    {
        singleChoice = true;
        window.SetButtonText(DefaultButtonText);
    }

    private bool IsSingleChoiceSelected(TMButton button)
    {
        var result = singleChoice && button.WasClicked();

        if (window && window.IsOpen) result |= inputManager.AnyKeyUp;

        return result;
    }
}
