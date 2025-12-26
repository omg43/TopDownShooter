using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    public class NavMeshMouseResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask = ~0;
        [SerializeField][Min(0)] private float m_raycastDistance = 1000f;
        [SerializeField][Min(0)] private float m_navMeshSampleMaxDistance = 100f;

        private Camera m_camera;

        public void Initialize(Camera camera)
        {
            m_camera = camera;
        }

        public Vector3? GetNavMeshPoint(Vector3 mousePosition)
        {
            var ray = m_camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, ~0))
            {
                if (NavMesh.SamplePosition(hit.point, out var navHit, 1000, NavMesh.AllAreas))
                {
                    return navHit.position;
                }
            }

            return null;
        }
    }
}

