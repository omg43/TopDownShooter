using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GamePlayEntryState : IState
{
    private PlayerController m_playerController;
    private readonly EnemySpawner m_spawnerEnemy;
    private readonly StateMashine m_stateMachine;
    private readonly AimLineMarker m_aimLineMarker;
    private readonly TargetMarkerObserver m_targetMarkerObserver;

    public GamePlayEntryState(
        StateMashine stateMachine,
        EnemySpawner spawnerEnemy,
        AimLineMarker aimLineMarker,
        TargetMarkerObserver targetMarkerObserver)
    {
        m_spawnerEnemy = spawnerEnemy;
        m_stateMachine = stateMachine;
        m_aimLineMarker = aimLineMarker;
        m_targetMarkerObserver = targetMarkerObserver;
    }

    public void Enter()
    {
        var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
        ServiceLocator.Resolve<IPlayerFactorySettings>().position = playerPosition.transform.position;
        m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create();

        m_aimLineMarker.Initialize(m_playerController.transform);
        m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());

        m_spawnerEnemy.Spawn();
        m_stateMachine.ChangedState<GamePlayState>();
    }

    public void Exit() { }

}