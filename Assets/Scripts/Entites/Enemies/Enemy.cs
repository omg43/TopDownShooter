using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;
    //TODO Add Movement
    [SerializeField] private HealthComponent m_health;
    [SerializeField] private EnemyData m_enemyData;
    private EnemyData m_data;
    private bool m_initialized;

    public IHealth Health => m_health;

    private void Awake()
    {
        Initialize(m_enemyData);
    }

    private void OnEnable()
    {
        m_health.ValuedChenged += () =>
        {
            Debug.Log($"Health Changed : {m_health.Value}");
        };
        m_health.Died += OnDied;
    }

    private void OnDisable()
    {
        
    }

    public void Initialize(EnemyData data)
    {
        if (m_initialized) { throw new InvalidOperationException("HealthComponent is Initialized!"); }
        m_data = data;
        m_health.Initialize(data.m_maxHealth);
    }
    private void OnDied() 
    {
         Died?.Invoke(this);
    }
}
