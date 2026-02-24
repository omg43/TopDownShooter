using Players;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarkerObserver : MonoBehaviour
    {
        [SerializeField] private TargetMarker m_targetMarker;
        [SerializeField] private PlayerMovement m_playerMovement;

        private PlayerMovement playerMovement
        {
            get 
            { 
                if(m_playerMovement is not null)
                {
                    return m_playerMovement;
                }            
            }
            set
            {
                m_playerMovement = value;
            }
        }

        private void OnEnable()
        {
            m_playerMovement.Stopped += OnPlayerStopped;
            m_playerMovement.DestinationChanged += OnDestinationChanged;
        }

        private void OnDisable()
        {
            m_playerMovement.Stopped -= OnPlayerStopped;
            m_playerMovement.DestinationChanged -= OnDestinationChanged;
        }

        private void OnPlayerStopped() =>
            m_targetMarker.Hide();

        private void OnDestinationChanged(Vector3 worldPosition) =>
            m_targetMarker.Show(worldPosition);
    }
}
