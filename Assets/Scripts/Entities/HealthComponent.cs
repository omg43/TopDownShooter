using Magic.Effects;
using System;
using UnityEngine;

namespace Entities
{
    internal class HealthComponent : MonoBehaviour, IHealth, IEffectable
    {
        public event Action Died;
        public event Action ValueChanged;

        private float m_value;
        private bool m_initialized;

        public float value
        {
            get => m_value;
            set
            {
                if (Mathf.Approximately(m_value, value))
                {
                    return;
                }

                m_value = value < 0 ? 0 : value;

                ValueChanged?.Invoke();

                if (m_value == 0)
                {
                    Died?.Invoke();
                }
            }
        }        

        public void Initialize(float value)
        {
            if (m_initialized)
            {
                throw new InvalidOperationException("Уже инициализирован");
            }

            m_initialized = true;            
            m_value = value;
        }

        public void Heal(float health)
        {
            if (health < 0)
                throw new ArgumentOutOfRangeException(nameof(health), health, "Отрицательное куда");

            value += health;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), damage, "Отрицательное куда");

            value -= damage;
        }
    }
}
