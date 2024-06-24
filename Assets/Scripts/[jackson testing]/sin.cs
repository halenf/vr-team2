using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sin : MonoBehaviour
{
    public float m_speed = 1;
    public float m_scale = 1;
    float m_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime * m_speed;
        if(m_time > Mathf.PI * 2)
            m_time = 0;
        if (m_time < 0)
            m_time = Mathf.PI * 2;

        transform.position = new Vector3(m_scale * Mathf.Cos(m_time), m_scale * Mathf.Sin(m_time), m_scale * Mathf.Sin(m_time));
    }
}
