using Entities.Enemies;
using UnityEngine;
using static StateMashine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner m_spawner;
    [SerializeField] private MainMenuView m_menuView;
    private void Awake()
    {
        var stateMachine = new StateMashine();
        stateMachine.Initialize(
            new MainMenuState(stateMachine, m_menuView),
            new GamePlayState(stateMachine),
            new PauseMenuState(stateMachine),
            new DeadState(stateMachine));
    }
}
