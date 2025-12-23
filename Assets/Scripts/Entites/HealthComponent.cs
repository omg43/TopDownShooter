using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth, IEffectable
{
    public event Action Died;
    public event Action ValuedChenged;
    
    private float m_value;

    public float Value
    {
        get => m_value;
        private set
        {
            if (Mathf.Approximately(m_value,value))
            {
                return;
            }
            m_value = value < 0 ? 0: value;

            ValuedChenged?.Invoke();

            if(m_value is 0)
            {
                Died?.Invoke();
            }
        }
    } 

    public void Initialize(float value)
    {
        m_value= value;
    }

    public void Heal(float heal)
    {
        if(heal < 0)
            throw new ArgumentOutOfRangeException(nameof(heal), "Heal cannot be negative");
        Value += heal; 
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Damage cannot be negative");
        Value -= damage;
    }
}
