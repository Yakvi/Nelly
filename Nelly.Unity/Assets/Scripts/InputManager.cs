using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : StandaloneInputModule
{
    //public UnityAction OnPlayerAction;
    public bool AnyKeyUp;
    public bool[] Actions;

    public GameObject LastObjectClicked;
    public GameObject HotObject;
    private bool anyKey;

    private void Update()
    {
        LastObjectClicked = GetLastObjectClicked();
        HotObject = GetHotObject();

        AnyKeyUp = GetAnyKeyUp();

        for (int i = 0; i < Actions.Length; i++)
        {
            var action = Input.GetAxis($"Action {i + 1}");
            Actions[i] = (action == 1) || (Input.GetKey(GetKeyCode(i)));
        }

        // if (Input.GetButtonDown("SysExit"))
        // {
        //     Application.Quit(0);
        // }
    }

    private bool GetAnyKeyUp()
    {
        var anyInProgress = Input.anyKey;

        var result = !anyInProgress && anyKey;

        anyKey = anyInProgress;

        return result;
    }

    public Vector2 GetPlayerInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public GameObject GetHotObject()
    {
        GameObject result = null;
        var pointerData = GetPointerData();

        if (pointerData != null)
        {
            result = pointerData.pointerPress;
        }

        return result;
    }

    private GameObject GetLastObjectClicked()
    {
        GameObject result = null;
        var pointerData = GetPointerData();

        if (pointerData != null &&
            !pointerData.eligibleForClick &&
            HotObject != null && pointerData.pointerEnter == HotObject)
        {
            result = pointerData.pointerEnter;
        }

        return result;
    }

    private static KeyCode GetKeyCode(int i)
    {
        KeyCode key = KeyCode.Space;
        switch (i)
        {
            case 0:
                key = KeyCode.Keypad1;
                break;
            case 1:
                key = KeyCode.Keypad2;
                break;
            case 2:
                key = KeyCode.Keypad3;
                break;
            case 3:
                key = KeyCode.Keypad4;
                break;
            default:
                Debug.LogError("Invalid Code Path");
                break;
        }

        return key;
    }

    private PointerEventData GetPointerData()
    {
        var result = m_PointerData.Count > 0 ? m_PointerData[kMouseLeftId] : null;
        return result;
    }
}
