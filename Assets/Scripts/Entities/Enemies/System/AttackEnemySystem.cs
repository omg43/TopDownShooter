using Magic.Spells.Data;
using Magic.Systems;
using UnityEngine;

public class AttackEnemySystem : MonoBehaviour
{
    private Transform m_Target;
    private BaseSpellData m_baseSpellData;
    private SpellCaster m_spellCaster;

    private float attackTime;
    private float m_cooldownTimer;

    private bool m_initialize;

    public void Inicialize(BaseSpellData spellData, Transform target, float timeAttack)
    {
        if (m_initialize == true)
            return;

        m_spellCaster = new SpellCaster(transform, true);
        m_baseSpellData = spellData;
        attackTime = timeAttack;
        m_Target = target;

        m_initialize = true;
    }

    private void Update()
    {
        if(!m_initialize)
            return;

        if (m_cooldownTimer > 0)
        {
            m_cooldownTimer -= Time.deltaTime;
        }
    }

    public bool TryAttack()
    {
        if(!m_initialize || m_Target == false)
        {
            return false;
        }

        if(m_cooldownTimer > 0)
        {
            return false;
        }

        m_spellCaster.Cast(m_baseSpellData, m_Target.position);
        m_cooldownTimer = attackTime;

        return true;
    }
}
