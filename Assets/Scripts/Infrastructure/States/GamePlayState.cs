using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using UnityEngine.InputSystem;

public class GamePlayState : IState
{
    private readonly StateMashine m_stateMachine;
    private readonly CameraFollow m_cameraFollow;

    private PlayerController m_playerController;

    public GamePlayState(
            StateMashine stateMachine,
            CameraFollow cameraFollow)
    {
        m_stateMachine = stateMachine;
        m_cameraFollow = cameraFollow;
    }

    public void Enter()
    {
        m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create();

        m_cameraFollow.SetTarget(m_playerController.transform);
        m_playerController.health.Died += OnDied;
    }

    public void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            m_stateMachine.ChangedState<PauseMenuState>();
        }
    }

    public void Exit()
        {
            m_playerController.health.Died -= OnDied;
            m_playerController = null;
    }

        private void OnDied()
        {
            m_stateMachine.ChangedState<DeadState>();
        }
    }
