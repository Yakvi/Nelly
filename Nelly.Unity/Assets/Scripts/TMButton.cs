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

    private GameObject buttonObj;
    private GameObject textObj;

    private InputManager inputManager;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        buttonObj = transform.Find("Button").gameObject;
        textObj = transform.Find("Button text").gameObject;
        textMesh = textObj.GetComponent<TextMeshProUGUI>();
        inputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();
    }

    public bool WasClicked()
    {
        var result = IsThisButton(inputManager.LastObjectClicked);
        return result;
    }

    public void SetText(string text = "")
    {
        IsActive = !String.IsNullOrWhiteSpace(text);
        buttonObj.SetActive(IsActive);
        textObj.SetActive(IsActive);

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
        var result = obj == buttonObj || obj == textObj;
        return result;
    }
}
