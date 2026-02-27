using Players;
using UnityEngine;
using static StateMashine;

namespace Infrastructure.States
{
    public class BootstrapState : MonoBehaviour, IState
    {
        [SerializeField] private MouseResolver m_mouseResolver;
        [SerializeField] private PlayerSpawnPoint m_playerSpawnPoint;

        private StateMashine m_stateMachine;

        public void Initialize(StateMashine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            ServiceLocator.Register(m_mouseResolver);

            var playerFactory = new PlayerFactory(GlobalConstants.Paths.PlayerPrefab);
            ServiceLocator.Register<IPlayerFactory>(playerFactory);
            ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);

            ServiceLocator.Register<PlayerSpawnPoint>(m_playerSpawnPoint);

            m_stateMachine.ChangedState<GamePlayState>();
        }

        public void Exit()
        {

        }
    }
}