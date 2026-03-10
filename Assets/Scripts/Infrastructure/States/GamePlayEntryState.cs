using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using UnityEngine;

public class GamePlayEntryState : IState
{
    [SerializeField] private PlayerController m_playerController;
    private StateMashine m_stateMachine;
    [SerializeField] private CameraFoll m_cameraFollow;
    private EnemySpawner m_enemySpawner;
    private AimLineMarker m_aimLineMarker;
    private TargetMarkerObserver m_targetMarkerObserver;

    public GamePlayEntryState(
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
        m_stateMachine.ChangedState<GamePlayState>();
    }

    public void Exit() { 
}
