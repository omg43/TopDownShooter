using Cameras;
using Entities.Enemies;
using Infrastructure.States;
using Markers;
using Players;
using UnityEngine;
using static StateMashine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private CameraFoll m_cameraFollow;
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;

        private void Awake()
        {
            var stateMachine = new StateMashine();
            m_bootStrapState.Initialize(stateMachine);

            stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(stateMachine),
                new DeadState(stateMachine, m_deadMenuView),
                new GamePlayState(
                    stateMachine,
                    m_cameraFollow,
                    m_enemySpawner,
                    m_aimLineMarker,
                    m_targetMarkerObserver));

            stateMachine.ChangedState<BootstrapState>();
        }
    }
}