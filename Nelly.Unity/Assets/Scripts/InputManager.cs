using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //public UnityAction OnPlayerAction;
    public bool isPressed = false;

    public Vector2 GetPlayerInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        isPressed = false;
        if (Input.anyKey)
        {
            isPressed = true;
            //OnPlayerAction.Invoke();
        }

        // if (Input.GetButtonDown("SysExit"))
        // {
        //     Application.Quit(0);
        // }
    }
}
