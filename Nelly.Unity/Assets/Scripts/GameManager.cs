using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Slide firstSlide;
    // private bool isPaused = false;
    public GameObject mainMenu;
    public GameObject MapOverlay;

    private TMButton dialogButton;
    private SlideManager slideManager;

    void Awake()
    {
        dialogButton = MapOverlay.transform.Find("Dialog Button").GetComponent<TMButton>();
        slideManager = gameObject.GetComponent<SlideManager>();

    }

    void Update()
    {
        if (dialogButton.WasClicked())
        {
            slideManager.ToggleRequested = true;
        }
    }

    public void NewGame()
    {
        mainMenu.SetActive(false);
    }
}
