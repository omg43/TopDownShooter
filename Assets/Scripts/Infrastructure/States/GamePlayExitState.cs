using Entities.Enemies;
using UnityEngine;

public class GamePlayExitState : IState
{
    public void Enter()
    {
        var loading = ServiceLocator.Resolve<Loading>();
        var spawner = ServiceLocator.Resolve<EnemySpawner>();
        spawner.DespawnAll();

        loading.LoadScene(GlobalConstants.Scenes.Main);
    }

    public void Exit()
    { 

    }
}
