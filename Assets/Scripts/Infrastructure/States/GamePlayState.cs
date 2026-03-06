using Cameras;
using Entities.Enemies;
using Markers;
using Players;

public class GamePlayState : IState
{
        private readonly StateMashine m_stateMachine;
        private readonly CameraFoll m_cameraFollow;
        private readonly EnemySpawner m_enemySpawner;
        private readonly AimLineMarker m_aimLineMarker;
        private readonly TargetMarkerObserver m_targetMarkerObserver;

        private PlayerController m_playerController;

        public GamePlayState(
            StateMashine stateMachine,
            CameraFoll cameraFollow,
            EnemySpawner enemySpawner,
            AimLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_stateMachine = stateMachine;
            m_cameraFollow = cameraFollow;
            m_enemySpawner = enemySpawner;
            m_aimLineMarker = aimLineMarker;
            m_targetMarkerObserver = targetMarkerObserver;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
            ServiceLocator.Resolve<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create().GetComponent<PlayerController>();

            m_cameraFollow.SetTarget(m_playerController.transform);
            m_aimLineMarker.Initialize(m_playerController.transform);
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());

            m_enemySpawner.Spawn();
            m_playerController.health.Died += OnDied;
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDied;
        }

        private void OnDied()
        {
            m_stateMachine.ChangedState<DeadState>();
        }
    }
