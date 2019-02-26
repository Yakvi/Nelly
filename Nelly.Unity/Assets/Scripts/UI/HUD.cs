using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TMButton[] ActiveButtons;
    public GameObject ButtonTemplate;
    
    public Vector2 FirstButtonOffset;

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
