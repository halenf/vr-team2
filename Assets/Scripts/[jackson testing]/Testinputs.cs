using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testinputs : MonoBehaviour
{
    public void OnBasic(InputAction.CallbackContext action)
    {
        if(action.started)
            Debug.Log("Input Started: " + action.control);
        if (action.performed)
            Debug.Log("Input Performed: " + action.control);
        if (action.canceled)
            Debug.Log("Input Caned: " + action.control);
    }
    public void OnLeft(InputAction.CallbackContext action)
    {
        Debug.Log("Left Input: " + action.control);
    }
    public void OnRight(InputAction.CallbackContext action)
    {
        Debug.Log("Right Input: " + action.control);
    }
    public void OnSticks(InputAction.CallbackContext action)
    {
        Debug.Log("Stick input");
    }
}
