using Entities.Enemies.Data;
using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

namespace Entities.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        [SerializeField] private EnemyData m_enemyData;
        [SerializeField] private AttackEnemySystem m_attackEnemySystem;
        [SerializeField] private HealthComponent m_health;
        [SerializeField] private EnemyStateMashine m_stateMashine;

        private Transform m_player;
        public HealthComponent health => m_health;

        private void Awake()
        {
            m_stateMashine = new EnemyStateMashine();
        }

        private void Update()
        {
            if(m_stateMashine.currentState is EnemyState.Dead || !m_enemyData)
            {
                return;
            }

            UpdateState();
        }

        public void Initialize(EnemyData data, Transform playerTransform)
        {
            m_enemyData = data;
            m_health.Initialize(data.health);
            m_attackEnemySystem.Inicialize(data.spell,playerTransform, data.attackTime);
            m_player = playerTransform;
        }

        private void UpdateState()
        {
            var isInAttackRange = IsInRange();

            switch (m_stateMashine.currentState)
            {
                case EnemyState.Idle:HandIdleState
            }
        }

        private void HandleIdleState(bool isInAttackRange)
        {
            if(m_enemyData.enemyType == AttackEnemyType.Range && isInAttackRange)
            {
                m_stateMashine.ChangeState(EnemyState.Attack);
            }
        }

        private void HendleAttackState(bool isInAttackRange)
        {
            m_attackEnemySystem.TryAttack();

            if(!)
        }

        private bool IsInRange()
        {
            if (!m_player)
            {
                return false;
            }
            var distance = Vector3.Distance(transform.position, m_player.position);
            return distance >= m_enemyData.attackRange;
        }

        private void OnEnable()
        {
            m_health.ValueChanged += () =>
            {
                Debug.Log($"Health Changed: {m_health.value}");
            };
            m_stateMashine.StateChange -= OnStateChanger;
            m_health.Died += OnDied;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
        }

        private void OnDied()
        {
            Died?.Invoke(this);
        }

        public void OnStateChanger(EnemyState previsionState, EnemyState nextState)
        {

        }
    }
}
