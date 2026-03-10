using UnityEngine;

public class GamePlayExitState : IState
{
    private readonly Loading m_loading;

    public void Enter()
    {

        var spawner = ServiceLocator.Resolve<Loading>();
        spawner.DespawnAll();

        m_loading.LoadScene(GlobalConstants.Scenes.Main);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
