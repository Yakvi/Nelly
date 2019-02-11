using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Slide firstSlide;
    // private bool isPaused = false;
    public GameObject mainMenu;
    public GameObject dialogArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        mainMenu.SetActive(false);
        dialogArea.SetActive(true);
    }
}
