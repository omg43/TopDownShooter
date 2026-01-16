using Entities.Enemies.Data;
using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

namespace Entities.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        [SerializeField] private AttackEnemySystem m_attackEnemySystem;
        [SerializeField] private HealthComponent m_health;
        [SerializeField] private EnemyMove m_enemyMove;

        private EnemyStateMashine m_stateMashine;
        private EnemyData m_enemyData;
        private Transform m_player;

        private void Awake()
        {
            m_stateMashine ??= new EnemyStateMashine();
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
            m_enemyMove.Initialize(data.speed, playerTransform);

            m_stateMashine ??= new EnemyStateMashine();

            if (m_enemyData.enemyType == AttackEnemyType.Melee)
            {
                m_stateMashine.ChangeState(EnemyState.Move);
            }
        }

        private void UpdateState()
        {
            var isInAttackRange = IsInRange();

            switch (m_stateMashine.currentState)
            {
                case EnemyState.Idle: HandleIdleState(isInAttackRange);break;
                case EnemyState.Attack: HandleAttackState(isInAttackRange);break;
                case EnemyState.Move: HandleMoveState(isInAttackRange);break;

            }
        }

        private void HandleIdleState(bool isInAttackRange)
        {
            if(m_enemyData.enemyType == AttackEnemyType.Range && isInAttackRange)
            {
                m_stateMashine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleMoveState(bool isInAttackRange)
        {
            if (isInAttackRange)
            {
                m_stateMashine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleAttackState(bool isInAttackRange)
        {
            m_attackEnemySystem.TryAttack();

            if (!isInAttackRange)
            {
                if(m_enemyData.enemyType == AttackEnemyType.Melee)
                {
                    m_stateMashine.ChangeState(EnemyState.Move);
                }
                else
                {
                    m_stateMashine.ChangeState(EnemyState.Idle);
                }
            }
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
            m_stateMashine.StateChange -= OnStateChanger;
        }

        private void OnDied()
        {
            Died?.Invoke(this);
        }

        public void OnStateChanger(EnemyState previsionState, EnemyState nextState)
        {
            if(previsionState is EnemyState.Move)
            {
                m_enemyMove.StopMoving();
            }

            if(nextState is EnemyState.Move)
            {
                m_enemyMove.StartMoving();
            }
        }
    }
}
