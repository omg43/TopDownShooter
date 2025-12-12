using System;
using Markers;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_agent;
        [SerializeField] private TargetMarker m_targetMarker;

        private float m_speed;
        
        private void OnValidate()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake()
        {
            Initialize(m_speed);
        }

        public void Initialize(float speed)
        {
            m_speed = speed;
            m_agent.speed = speed;
        }
        
        public void SetDestination(Vector3 navMeshPoint)
        {
            m_targetMarker.Show(navMeshPoint);
            m_agent.SetDestination(navMeshPoint);
        }
    }
}