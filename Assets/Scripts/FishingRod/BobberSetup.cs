using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.SetParent(null);
        transform.rotation = Quaternion.identity;
    }
}
