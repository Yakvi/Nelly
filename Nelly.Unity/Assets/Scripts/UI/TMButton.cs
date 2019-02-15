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

    public GameObject ButtonSprite;
    public GameObject ButtonText;

    private InputManager inputManager;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = ButtonText.GetComponent<TextMeshProUGUI>();
        inputManager = FindObjectOfType<InputManager>();
    }

    public bool WasClicked()
    {
        var result = IsThisButton(inputManager.LastObjectClicked);
        return result;
    }

    public void SetText(string text = "")
    {
        IsActive = !String.IsNullOrWhiteSpace(text);
        ButtonSprite.SetActive(IsActive);
        ButtonText.SetActive(IsActive);

        textMesh.text = text;
    }

    public bool IsHot(int action)
    {
        var result = inputManager.AnyKeyUp &&
            (inputManager.Actions[action] || WasClicked());
        return result;
    }

    private bool IsThisButton(GameObject obj)
    {
        var result = obj == ButtonSprite || obj == ButtonText;
        return result;
    }
}
