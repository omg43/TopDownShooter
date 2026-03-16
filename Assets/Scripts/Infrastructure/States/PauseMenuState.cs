using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PauseMenuState : IState
{
    private readonly StateMashine m_stateMachine;
    private readonly PauseMenuView m_pauseMenuView;

    public PauseMenuState(
        StateMashine stateMachine,
        PauseMenuView pauseMenuView)
    {
        m_stateMachine = stateMachine;
        m_pauseMenuView = pauseMenuView;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        m_pauseMenuView.gameObject.SetActive(true);
        m_pauseMenuView.ContinueClicked += OnContinueClicked;
        m_pauseMenuView.MainMenuClicked += OnMainMenuClicked;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        m_pauseMenuView.gameObject.SetActive(false);
        m_pauseMenuView.ContinueClicked -= OnContinueClicked;
        m_pauseMenuView.MainMenuClicked -= OnMainMenuClicked;
    }

    private void OnContinueClicked() =>
        m_stateMachine.ChangedState<GamePlayState>();

    private void OnMainMenuClicked()
    {
        m_stateMachine.ChangedState<GamePlayState>();
    }
}
