using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;
using System;
//using UnityEngine.EventSystems;

public class TMButton : MonoBehaviour
{
    public bool IsActive;
    public bool WasClicked;

    public GameObject Button;
    public GameObject Text;

    private InputManager inputManager;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = Text.GetComponent<TextMeshProUGUI>();
        inputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();
    }

    void Update()
    {
        WasClicked = IsThisButton(inputManager.LastObjectClicked);
    }

    public void SetText(string text = "")
    {
        IsActive = !String.IsNullOrWhiteSpace(text);
        Button.SetActive(IsActive);
        Text.SetActive(IsActive);

        textMesh.text = text;
    }

    public bool IsHot(int action)
    {
        var result = inputManager.AnyKey &&
            (inputManager.Actions[action] || WasClicked);
        return result;
    }

    private bool IsThisButton(GameObject obj)
    {
        var result = obj == Button || obj == Text;
        return result;
    }
}
