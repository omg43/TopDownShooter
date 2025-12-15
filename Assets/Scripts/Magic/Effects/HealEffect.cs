using System;
using UnityEngine;

[Serializable]
public class HealEffect : IEffect
{
    [SerializeField] private float m_health;

    public void Apply(IEffectable effectable)
    {
        if(effectable is IHealth health)
        {
            health.Heal(m_health);
        }
    }
}