using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private IState m_state;
    private Dictionary<Type, IState> m_states = new();

    public void Initialize(params IState[] states)
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

public interface IState
{
    public void Enter();

    public void Exit(); 
}
