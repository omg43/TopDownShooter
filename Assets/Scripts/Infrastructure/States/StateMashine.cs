using Entities.Enemies;
using Players;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private IState m_state;
    private Dictionary<Type, IState> m_states = new();

    public void Initialize(IState[] states)
    {
        if (m_states.Count > 0) return;

        foreach (var state in states)
        {
            m_states.Add(state.GetType(), state);
        }
    }

    public void ChangedState<T>()
        where T : IState
    {
        m_state?.Exit();
        {
            m_state = m_states[typeof(T)];
        }
        m_state.Enter();
    }

    public class MainMenuState : IState
    {
        private readonly StateMashine m_stateMashine;
        private readonly MainMenuView m_mainMenuView;

        public MainMenuState(StateMashine stateMashine, MainMenuView mainMenuView)
        {
            m_stateMashine = stateMashine;
            m_mainMenuView = mainMenuView; 

            m_mainMenuView.gameObject.SetActive(false);
        }

        public void Enter()
        {
            m_mainMenuView.gameObject.SetActive(true);
            m_mainMenuView.PlayClicked += OnPlayClicked;
            m_mainMenuView.ExitClicked += OnExitClecked;
        }

        private void OnPlayClicked() =>
            m_stateMashine.ChangedState<GamePlayState>();

        private void OnExitClecked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif

            Application.Quit();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
    public class GamePlayState : IState
    {
        private readonly StateMashine m_stateMachine;
        private readonly EnemySpawner m_enemySpawner;
        private readonly PlayerController m_playerController;

        public GamePlayState(
            StateMashine stateMachine,
            EnemySpawner enemySpawner,
            PlayerController playerController)
        {
            m_stateMachine = stateMachine;
            m_enemySpawner = enemySpawner;
            m_playerController = playerController;
        }

        public void Enter()
        {
            ServiceLocator.Register(m_mouse);

            var playerFactory = new PlayerFactory("Prefabs/Player");

            ServiceLocator.Resolve<IPlayerFactorySettings>().position = m_playerController;
            ServiceLocator.Resolve<PlayerFactory>.Creat(playerFactory);

            ServiceLocator.Register(m_playerController);
            m_enemySpawner.Spawn();
            m_playerController.health.Died += OnDied;
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDied;
        }

        private void OnDied() =>
            m_stateMachine.ChangedState<MainMenuState>();
    }
    public class PauseMenuState : IState
    {
        private readonly StateMashine m_stateMashine;

        public PauseMenuState(StateMashine stateMashine)
        {
            m_stateMashine = stateMashine;
        }
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
    public class DeadState : IState
    {
        private readonly StateMashine m_stateMashine;
        private readonly DeadMenuView m_deadView;

        public DeadState(StateMashine stateMashine, DeadMenuView deadView)
        {
            m_stateMashine = stateMashine;
            m_deadView = deadView;
        }
        public void Enter()
        {
            m_deadView.GoToMenuClicked += OnGoToMenuClicked;
            m_deadView.gameObject.SetActive(true);
        }

        private void OnGoToMenuClicked() =>
            m_stateMashine.ChangedState<MainMenuState>();

        public void Exit()
        {
            m_deadView.GoToMenuClicked -= OnGoToMenuClicked;
            m_deadView.gameObject.SetActive(false);
        }
    }

    public class BoostrapState : MonoBehaviour, IState 
    {
        [SerializeField] private MouseResolver m_mouseResolver;

        public void Enter()
        {
            ServiceLocator.Register(m_mouseResolver);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}

public interface IState
{
    public void Enter();

    public void Exit(); 
}
