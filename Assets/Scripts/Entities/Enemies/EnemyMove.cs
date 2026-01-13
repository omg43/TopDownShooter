using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent;

    private Transform m_target;
    private bool m_isMoving;
    private bool m_isInitializing;

    private void OnValidate()
    {
        if (!m_agent)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }

    public void Initialize(float speed, Transform target)
    {
        m_target = target;
        m_agent.speed = speed;
        m_isInitializing = true;
    }

    private void Update()
    {
        if(!m_isInitializing || !m_isMoving || !m_target)
        {
            return;
        }

        m_agent.SetDestination(m_target.position);
    }

    public void StartMoving()
    {
        if (!m_isInitializing)
        {
            return;
        }

        m_isMoving = true;
        m_agent.isStopped = true;
    }

    public void StopMoving()
    {
        if (!m_isInitializing)
        {
            return;
        }

        m_isMoving = true;
        m_agent.isStopped = true;
        m_agent.velocity = Vector3.zero;
    }
}
