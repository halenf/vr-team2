using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class motiontest : MonoBehaviour
{
    public void Move(InputAction.CallbackContext action)
    {
        var value = action.ReadValue<Vector3>();
        Debug.Log(value);
    }
}
