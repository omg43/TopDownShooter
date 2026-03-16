using Cameras;
using Entities.Enemies;
using Infrastructure.States;
using Markers;
using Players;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static StateMashine;

namespace Infrastructure
{
    public class BootstrapGamePlay : MonoBehaviour
    {
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private PauseMenuView m_pauseMenuView;

        private StateMashine m_stateMachine;

        private void Awake()
        {
            m_stateMachine = new StateMashine();
            m_bootStrapState.Initialize(m_stateMachine);

            m_stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(m_stateMachine, m_pauseMenuView),
                new DeadState(m_stateMachine, m_deadMenuView),
                new GamePlayState(m_stateMachine, m_cameraFollow),
                new GamePlayExitState(),
                new GamePlayEntryState(
                    m_stateMachine,
                    m_enemySpawner,
                    m_aimLineMarker,
                    m_targetMarkerObserver));

            m_stateMachine.ChangedState<BootstrapState>();
        }
    }
}