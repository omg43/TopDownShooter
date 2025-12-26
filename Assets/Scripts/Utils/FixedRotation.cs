using UnityEngine;

namespace Utils
{
    internal class FixedRotation : MonoBehaviour
    {
        private Transform m_parent;
        private Quaternion m_rotation;
        private Vector3 m_worldOffset;

        private void Start()
        {
            m_parent = transform.parent;

            m_rotation = transform.rotation;
            m_worldOffset = transform.position - m_parent.position;
        }

        private void LateUpdate()
        {
            if (m_parent == null) return;

            transform.position = m_parent.position + m_worldOffset;
            transform.rotation = m_rotation;                   
        }
    }
}
