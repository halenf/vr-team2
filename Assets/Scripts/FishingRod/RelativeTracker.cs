using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeTracker : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    private Vector2 m_direction;
    private float m_rotVelo = 0.0f;
    public float debug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //assume z lock
        m_direction = (new Vector2(m_target.position.x, m_target.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        m_rotVelo = Mathf.Atan(m_direction.y / m_direction.x);
        debug = Mathf.Rad2Deg * m_rotVelo;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, m_direction + new Vector2(transform.position.x, transform.position.y));
    }
}
