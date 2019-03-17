using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SlideManager : MonoBehaviour
{
    public string DefaultButtonText = "";

    public DialogWindow FullscreenWindow;
    // public DialogWindow BordersWindow;

    [HideInInspector]
    public PlayerChoice LastInteraction;
    public DialogWindow ActiveWindow;

    private Canvas canvas;
    private MapManager mapManager;
    private GameManager gameManager;

    private bool singleChoice;
    private bool soundsOn;

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        mapManager = gameObject.GetComponent<MapManager>();
        gameManager = gameObject.GetComponent<GameManager>();

        // TODO: DEBUG ONLY! Remove on release
        // ActiveWindow = Instantiate(FullscreenWindow, canvas.transform);
    }

    void Update()
    {
        LastInteraction = PlayerChoice.None;
        if (ActiveWindow)
        {
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
    }

    public void Restart()
    {
        if (ActiveWindow) GameObject.Destroy(ActiveWindow.gameObject);
        ActiveWindow = Instantiate(FullscreenWindow, canvas.transform);
    }

    public void ToggleSounds(bool playSounds)
    {
        soundsOn = playSounds;

        if (playSounds)
        {
            gameManager.AmbientPlayer.Play();
        }
        else
        {
            gameManager.FXPlayer.Stop();
            gameManager.AmbientPlayer.Stop();
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
                LastInteraction = (PlayerChoice) i;
                break;
            }
        }
    }

    public void ChangeSlide(Slide slideData)
    {
        if (ActiveWindow)
        {
            ClearSlide();

            // Rendering
            if (slideData != null)
            {
                ActiveWindow.SetTitle(slideData.ImageText);
                ActiveWindow.SetSubtitle(slideData.DialogText);
                ActiveWindow.SetPicture(slideData.Image, slideData.ImageTint);
                SetButtons(slideData.Choices, slideData.IsLinear());

                PlaySounds(slideData);
                mapManager.ChangePlayerPosition(slideData.PlayerPosition);
            }
            else
            {
                ActiveWindow.SetSubtitle("Slide data is not found. GG.");
                Application.Quit();
            }
        }
    }

    private void ClearSlide()
    {
        singleChoice = false;
        ActiveWindow.Clear();
        mapManager.ClearTempPOI();
    }

    private void PlaySounds(Slide slideData)
    {
        if (slideData.Sound != null)
        {
            gameManager.FXPlayer.clip = slideData.Sound;
            gameManager.FXPlayer.volume = slideData.SoundVolume;
            gameManager.FXPlayer.Play();
        }
        if (slideData.Ambient != null)
        {
            gameManager.AmbientPlayer.clip = slideData.Ambient;
            gameManager.AmbientPlayer.volume = slideData.AmbientVolume;
            gameManager.AmbientPlayer.Play();
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
                    if (choice.POI != null) mapManager.SetPOI(choice);
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
        var result = singleChoice &&
            (button.WasClicked() || ActiveWindow.KeyUpCaptured);

        return result;
    }
}
