using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    private int m_lineLockPhase = 0;
    public int isLineLocked { 
        get { return m_lineLockPhase; }
        set { m_lineLockPhase = value; } }
    private Vector3 m_handVelocity;
    [SerializeField] private float m_forceScale;
    public Vector3 setHandVelocity { set { m_handVelocity = value; } }
    [SerializeField] private GameObject m_hook;
    [SerializeField] private Transform m_hookHold;

    // Update is called once per frame
    void Update()
    {
        if(m_lineLockPhase == 1)
        {
            m_hook.transform.position = Vector3.Slerp(m_hook.transform.position, m_hookHold.position, 0.5f);
            m_hook.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (m_lineLockPhase == 2)
        {
            //cast rod out
            m_hook.GetComponent<Rigidbody>().isKinematic = false;
            m_hook.GetComponent<Rigidbody>().AddForce(m_handVelocity * m_forceScale);
            m_lineLockPhase = 0;
        }
    }
}
