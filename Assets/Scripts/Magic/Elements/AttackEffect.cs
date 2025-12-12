using UnityEngine;

public class AttackEffect : IEffect
{
    [SerializeField][Min(0)] private float m_damage;

    public void Apply(IEffectble effectble)
    {
        if(effectble is IHealth health)
        {
            health.TakeDamage(m_damage);
        }
    }
}
