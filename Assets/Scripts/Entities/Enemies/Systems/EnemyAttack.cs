using Magic.Spells.Data;
using Magic.Systems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Enemies.Systems
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        private Transform m_target;
        private IReadOnlyList<SpellEnemyData> m_spells;
        private SpellCaster m_spellCaster;
        private BaseSpellData m_defaultSpell;

        private float m_attackTime;
        private float m_cooldownTimer;

        private bool m_isInitialized;

        private int m_count;
        private int m_maxCount;

        public void Initialize(IReadOnlyList<SpellEnemyData> spells, Transform target, float attackTime, BaseSpellData defaultSpellData)
        {
            if (m_isInitialized)
            {
                return;
            }

            m_target = target;
            m_attackTime = attackTime;
            m_defaultSpell = defaultSpellData;
            m_spells = spells.OrderBy(spell => spells.Count).ToArray();
            m_spellCaster = new SpellCaster(transform, true);

            m_maxCount = spells.LastOrDefault().count;
            m_isInitialized = true;
        }

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target)
            {
                return false;
            }

            if (m_cooldownTimer > 0)
            {
                return false;
            }

            m_count++;
            var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

            if(spell.spell is null)
            {
                m_spellCaster.Cast(m_spells[0].spell, m_target.position);
            }
            else
            {
                m_spellCaster.Cast(spell.spell, m_target.position);
            }

            m_cooldownTimer = m_attackTime;

            return true;
        }


    }
}