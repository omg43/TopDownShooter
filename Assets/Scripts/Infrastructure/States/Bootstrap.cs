using Entities.Enemies;
using Infrastructure.States;
using Players;
using UnityEngine;
using static StateMashine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BoostrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;

        private void Awake()
        {
            var stateMachine = new StateMashine();
            m_bootStrapState.(stateMachine);

            stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(stateMachine),
                new DeadState(stateMachine, m_deadMenuView),
                new GamePlayState(stateMachine, m_enemySpawner, m_playerController));

            stateMachine.ChangedState<BootstrapState>();
        }
    }
}