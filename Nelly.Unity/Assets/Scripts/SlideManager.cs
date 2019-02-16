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

    public DialogWindow FullscreenWindow;
    // public DialogWindow BordersWindow;

    [HideInInspector]
    public Selection LastInteraction;
    public DialogWindow ActiveWindow;

    private Canvas canvas;
    private MapManager mapManager;

    private AudioSource fxSound;
    private AudioSource ambientSound;
    private bool singleChoice;
    private bool soundsOn;

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        mapManager = gameObject.GetComponent<MapManager>();

        fxSound = GameObject.Find("FXSound").GetComponent<AudioSource>();
        ambientSound = GameObject.Find("AmbientSound").GetComponent<AudioSource>();

        ActiveWindow = Instantiate(FullscreenWindow, canvas.transform);
    }

    void Update()
    {
        LastInteraction = Selection.None;
        var windowOpen = ActiveWindow.IsOpen();

        if (windowOpen)
        {
            ProcessInteractions();
        }

        if (windowOpen != soundsOn)
        {
            ToggleSounds(windowOpen);
        }
    }

    public void ToggleSounds(bool playSounds)
    {
        soundsOn = playSounds;

        if (playSounds)
        {
            ambientSound.Play();
        }
        else
        {
            fxSound.Stop();
            ambientSound.Stop();
        }
    }

    public void ProcessInteractions()
    {
        for (int i = 0; i < ActiveWindow.ButtonCount; i++)
        {
            var button = ActiveWindow.GetButton(i);
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
        ActiveWindow.Clear();
        mapManager.ClearTempPOI();

        // Rendering
        if (slideData != null)
        {
            ActiveWindow.SetTitle(slideData.ImageText);
            ActiveWindow.SetSubtitle(slideData.DialogText);
            ActiveWindow.SetPicture(slideData.Image, slideData.ImageTint);
            SetButtons(slideData.Choices, slideData.IsLinear());

            PlaySounds(slideData);
            mapManager.ChangePlayerPosition(slideData.NewLocation);
        }
        else
        {
            ActiveWindow.SetSubtitle("Slide data is not found. GG.");
            Application.Quit();
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
            for (int i = 0; i < ActiveWindow.ButtonCount; i++)
            {
                if (i < choices.Length && choices[i])
                {
                    var choice = choices[i];
                    ActiveWindow.SetButtonText(choice.Text, i);
                    mapManager.SetPOI(choice);
                }
            }
        }
    }

    private void AddSingleChoiceButton()
    {
        singleChoice = true;
        ActiveWindow.SetButtonText(DefaultButtonText);
    }

    private bool IsSingleChoiceSelected(TMButton button)
    {
        var result = singleChoice && button.WasClicked();

        // Skip 1 keypress if window was just opened
 
        if (ActiveWindow) result |= ActiveWindow.KeyUpCaptured;
        
        return result;
    }
}
