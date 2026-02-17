using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private IState m_state;
    private Dictionary<Type, IState> m_states = new();

    public  void Initialize(IState[] states)
    {
        if (m_states.Count > 0) return;

        foreach (var state in states)
        {
            m_states.Add(state.GetType(), state);
        }
    }

    public void ChangedState<T>()
        where T: IState
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
        }

        public void Enter()
        {
            m_mainMenuView.gameObject.SetActive(true);
            m_mainMenuView.PlayClicked += OnPlayClicked;
        }

        private void OnPlayClicked()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
    public class GamePlayState : IState
    {
        private readonly StateMashine m_stateMashine;

        public GamePlayState(StateMashine stateMashine)
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

        public DeadState(StateMashine stateMashine)
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
}

public interface IState
{
    public void Enter();

    public void Exit(); 
}
