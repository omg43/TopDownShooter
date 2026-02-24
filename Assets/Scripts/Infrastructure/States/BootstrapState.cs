using Players;
using System.IO;
using UnityEngine;
using static StateMashine;

namespace Infrastructure.States
{
    public class BootstrapState : MonoBehaviour, IState
    {
        [SerializeField] private MouseResolver m_mouseResolver;

        private StateMashine m_stateMachine;

        public void Initialize(StateMashine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            ServiceLocator.Register(m_mouseResolver);

            var playerFactory = new PlayerFactory("Prefabs/Player");

            ServiceLocator.Register<PlayerFactory>(playerFactory);
            ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);

            m_stateMachine.ChangedState<GamePlayState>();
        }

        public void Exit()
        {

        }
    }
}