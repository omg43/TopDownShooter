using Magic.Spells.Data;
using UnityEngine;

namespace Entities.Enemies.Data
{

    [CreateAssetMenu(fileName = "EnemyData", menuName = "XLab/Enemies/Enemy")]
    internal class EnemyData : ScriptableObject
    {
        [SerializeField] private AttackEnemyType m_enemyType;
        
        [Header("Parameters")]
        [SerializeField][Min(0f)] private float m_health;
        [SerializeField][Range(0f, 100f)] private float m_speed;

        [Header("Attack")]
        [SerializeField] private BaseSpellData m_spell;
        [SerializeField][Min(0)] private float m_attackTime;        
        [SerializeField][Min(0)] private float m_attackRange;

        public BaseSpellData spell => m_spell;
        public AttackEnemyType enemyType => m_enemyType;
        public float health => m_health;
        public float speed => m_speed;
        public float attackTime => m_attackTime;
        public float attackRange => m_attackRange;

    }
}
