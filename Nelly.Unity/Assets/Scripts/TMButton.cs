using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;
using System;
//using UnityEngine.EventSystems;

public class TMButton : MonoBehaviour
{
    public bool IsClicked;

    public GameObject Button;
    public GameObject Text;

    private InputManager inputManager;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = Text.GetComponent<TextMeshProUGUI>();
        inputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();
    }

    void Update()
    {
        var hotObject = inputManager.GetHotObject();
        IsClicked = hotObject && (hotObject == Button || hotObject == Text);
    }

    public void SetText(string text = "")
    {
        var isEmpty = String.IsNullOrWhiteSpace(text);
        Button.SetActive(!isEmpty);
        Text.SetActive(!isEmpty);

        textMesh.text = text;
    }

    internal bool IsHot(int action)
    {
        var result = inputManager.Actions[action] || IsClicked;
        return result;
    }
}
