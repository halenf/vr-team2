using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropdownReader : MonoBehaviour
{
    public UnityEvent[] events = new UnityEvent[1];
    private void Start()
    {
        events[0].Invoke();
    }
    public void InvokeEvent(int value)
    {
        events[value].Invoke();
    }
}
