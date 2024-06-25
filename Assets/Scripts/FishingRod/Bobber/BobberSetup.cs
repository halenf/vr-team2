using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace FishingRod
    {

        public class BobberSetup : MonoBehaviour
        {
            Rigidbody m_thisRb;
            Rigidbody m_childRb;
            // Start is called before the first frame update
            void Awake()
            {
                //Match the rbs with the child
                m_thisRb = transform.GetComponent<Rigidbody>();
                m_childRb = transform.GetChild(0).GetComponent<Rigidbody>();
                m_thisRb.mass = m_childRb.mass;
                m_thisRb.drag = m_childRb.drag;
                m_thisRb.angularDrag = m_childRb.angularDrag;
                //unparent this
                transform.SetParent(null);
                //reset rotation
                transform.rotation = Quaternion.identity;
                //remove this component
                Destroy(this);
            }
        }
    }
}
