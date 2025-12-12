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
        private float m_angleSpeed;
        
        private void OnValidate()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake()
        {
            Initialize(m_speed,m_angleSpeed);
        }

        public void Initialize(float speed, float angleSpeed)
        {
            m_speed = speed;
            m_agent.speed = speed;

            m_agent.angularSpeed = angleSpeed;
            m_angleSpeed = angleSpeed;

            m_agent.updateRotation = false;
        }
        
        public void SetDestination(Vector3 navMeshPoint)
        {
            m_targetMarker.Show(navMeshPoint);
            m_agent.SetDestination(navMeshPoint);
        }

        public void RotationTowards(Vector3 worldPoint)
        {
            var direction = worldPoint - transform.position;
            direction.y = 0;

            if(direction.sqrMagnitude < 0.0001f)
            {
                return;
            }

            var transformRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,transformRotation,m_agent.angularSpeed * Time.deltaTime);
        }
    }
}