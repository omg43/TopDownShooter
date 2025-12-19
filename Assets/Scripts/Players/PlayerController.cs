using Assets.Scripts.Players;
using Magic.Systems;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private MouseResolver m_navMeshMouseResolver;
        [SerializeField] private MagicInputHelper m_magicInputHelper;

        private PlayerRotationCalculeter m_playerRotationCalculeter;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }
            
            if (!m_navMeshMouseResolver)
            {
                m_navMeshMouseResolver = GetComponent<MouseResolver>();
            }
        }

        private void Start()
        {
            var camera = Camera.main;

            m_playerRotationCalculeter = new PlayerRotationCalculeter(camera, transform);
            m_playerMovement.Initialize(m_config.speed);
            
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
                    
                    Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint();
                    if (navPoint.HasValue)
                    {
                        m_playerMovement.SetDestination(navPoint.Value);
                    }
                }
            }
            m_magicInputHelper.Update();
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
