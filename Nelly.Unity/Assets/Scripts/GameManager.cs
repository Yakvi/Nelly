using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public Slide firstSlide;
    // private bool isPaused = false;
    public HUD MainMenu;
    public HUD Map;

    public AudioSource MusicPlayer;
    public AudioSource AmbientPlayer;
    public AudioSource FXPlayer;

    private SlideManager slideManager;
    private DialogManager dialogManager;

    private TMButton newGameButton;
    private TMButton dialogButton;

    void Awake()
    {
        newGameButton = MainMenu.ActiveButtons[0];
        dialogButton = Map.ActiveButtons[0];

        slideManager = gameObject.GetComponent<SlideManager>();
        dialogManager = gameObject.GetComponent<DialogManager>();
    }

    void Update()
    {
        if (newGameButton.WasClicked())
        {
            NewGame();
        }
        
        if (dialogButton.WasClicked())
        {
            slideManager.ActiveWindow.Toggle();
        }
    }

    void NewGame()
    {
        MainMenu.Disable();
        // dialogManager.Restart(); For now, we don't enable dialog window by default
    }
}
