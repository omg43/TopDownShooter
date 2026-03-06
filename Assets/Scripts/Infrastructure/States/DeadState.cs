using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(GlobalConstants.Scenes.Main);

    public void Exit()
        {
            m_deadView.GoToMenuClicked -= OnGoToMenuClicked;
            m_deadView.gameObject.SetActive(false);
        }
    }
