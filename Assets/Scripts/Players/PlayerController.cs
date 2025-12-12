using Assets.Scripts.Players;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(NavMeshMouseResolver))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private NavMeshMouseResolver m_navMeshMouseResolver;

        private PlayerRotationCalculeter m_playerRotationCalculeter;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }
            
            if (!m_navMeshMouseResolver)
            {
                m_navMeshMouseResolver = GetComponent<NavMeshMouseResolver>();
            }
        }

        private void Start()
        {
            var camera = Camera.main;

            m_playerRotationCalculeter = new PlayerRotationCalculeter(camera, transform);
            m_playerMovement.Initialize(m_config.speed, m_config.angularSpeed);
            m_navMeshMouseResolver.Initialize(camera);
            
            SetupeCursore();
        }

        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculeter.Calculate(mousePosition);
            m_playerMovement.RotationTowards(lookPoint);
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {

                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    
                    Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint(mousePosition);
                    if (navPoint.HasValue)
                    {
                        m_playerMovement.SetDestination(navPoint.Value);
                    }
                }
            }
        }

        private void SetupeCursore()
        {
            var texture = m_config.cursoreTexture;
            if (texture)
            {
                var hotspot = new Vector2(texture.width/2f, texture.height/2f);
                Cursor.SetCursor(texture, hotspot ,CursorMode.Auto);
            }
        }
    }
}
