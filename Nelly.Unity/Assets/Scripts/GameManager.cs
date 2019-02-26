using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public Slide firstSlide;
    // private bool isPaused = false;
    public GameObject MainMenuUI;
    public GameObject MapHUD;

    public AudioSource MusicPlayer;
    public AudioSource AmbientPlayer;
    public AudioSource FXPlayer;

    private TMButton dialogButton;
    private SlideManager slideManager;

    void Awake()
    {
        dialogButton = MapHUD.transform.Find("Dialog Button").GetComponent<TMButton>();
        slideManager = gameObject.GetComponent<SlideManager>();

    }

    void Update()
    {
        if (dialogButton.WasClicked())
        {
            slideManager.ActiveWindow.Toggle();
        }
    }

    public void NewGame()
    {
        MainMenuUI.SetActive(false);
    }
}
