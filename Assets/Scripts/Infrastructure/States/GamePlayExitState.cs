using Entities.Enemies;
using UnityEngine;

public class GamePlayExitState : IState
{
    private readonly EnemySpawner m_spawnerEnemy;

    public GamePlayExitState(EnemySpawner spawnerEnemy)
    {
        m_spawnerEnemy = spawnerEnemy;
    }

    public void Enter()
    {
        var loading = ServiceLocator.Resolve<Loading>();
        m_spawnerEnemy.DespawnAll();

        loading.LoadScene(GlobalConstants.Scenes.Main);
    }

    public void Exit() { }
}
