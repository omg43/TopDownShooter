using System;

public class PauseMenuState : IState
{
    private readonly StateMashine m_stateMashine;
    private PauseMenuView m_pauseMenuView;
    public PauseMenuState(StateMashine stateMashine, PauseMenuView pauseMenuView)
    {
        m_stateMashine = stateMashine;
        m_pauseMenuView = pauseMenuView;
    }
    public void Enter()
    {
        m_pauseMenuView.gameObject.SetActive(true);
        m_pauseMenuView.CountieClicked += OnCountClicked();
        m_pauseMenuView.MainMenuClicked += OnMainMenuClicked();
    }

    private Action OnMainMenuClicked()
    {
        throw new NotImplementedException();
    }

    private Action OnCountClicked()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }
}
