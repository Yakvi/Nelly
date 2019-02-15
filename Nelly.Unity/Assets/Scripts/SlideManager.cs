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

    public DialogWindow FullscreenWindow;
    // public DialogWindow BordersWindow;

    [HideInInspector]
    public bool ToggleRequested;
    [HideInInspector]
    public Selection LastInteraction;

    private DialogWindow activeWindow;
    private Canvas canvas;
    private MapManager mapManager;
    private InputManager inputManager;

    private AudioSource fxSound;
    private AudioSource ambientSound;
    private bool singleChoice;
    private bool soundsOn;

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        canvas = FindObjectOfType<Canvas>();
        mapManager = gameObject.GetComponent<MapManager>();

        fxSound = GameObject.Find("FXSound").GetComponent<AudioSource>();
        ambientSound = GameObject.Find("AmbientSound").GetComponent<AudioSource>();

        activeWindow = Instantiate(FullscreenWindow, canvas.transform);
    }

    void Update()
    {
        LastInteraction = Selection.None;

        if (ToggleRequested)
        {
            ToggleRequested = false;
            activeWindow.Toggle();
        }
        else
        {
            ProcessInteractions();
        }

        if (activeWindow.IsOpen != soundsOn)
        {
            soundsOn = activeWindow.IsOpen;
            ToggleSounds();
        }
    }

    public void ToggleSounds()
    {
        if (!activeWindow.IsOpen)
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
        for (int i = 0; i < activeWindow.ButtonCount; i++)
        {
            var button = activeWindow.GetButton(i);
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
        activeWindow.Clear();

        // Rendering
        if (slideData != null)
        {
            activeWindow.SetTitle(slideData.ImageText);
            activeWindow.SetSubtitle(slideData.DialogText);
            activeWindow.SetPicture(slideData.Image, slideData.ImageTint);
            SetButtons(slideData.Choices, slideData.IsLinear());

            PlaySounds(slideData);
            ChangeLocation(slideData.NewLocation);
        }
        else
        {
            activeWindow.SetSubtitle("Slide data is not found. GG.");
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

    private void SetButtons(Choice[] choices, bool slideIsLinear)
    {
        if (slideIsLinear)
        {
            AddSingleChoiceButton();
        }
        else
        {
            for (int i = 0; i < activeWindow.ButtonCount; i++)
            {
                var choice = choices[i];
                if (i < choices.Length && choice != null)
                {
                    activeWindow.SetButtonText(choice.Text, i);
                    //mapManager.SetPOI(choice);
                }
            }
        }
    }

    private void AddSingleChoiceButton()
    {
        singleChoice = true;
        activeWindow.SetButtonText(DefaultButtonText);
    }

    private bool IsSingleChoiceSelected(TMButton button)
    {
        var result = singleChoice && button.WasClicked();

        if (activeWindow && activeWindow.IsOpen) result |= inputManager.AnyKeyUp;

        return result;
    }
}
