using UnityEngine;

[CreateAssetMenu(fileName = ("EnemyData"), menuName =("Enemies"))]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public AttackEnemyType m_enemyType { get; private set; }
   

    [Header("Parameters")]
    [field: SerializeField][Min(0)] public float m_maxHealth { get; private set; }
    [field: SerializeField][Range(0, 100)] public float m_speed { get; private set; }
    [field: SerializeField][Min(0)] public float m_coolDown { get; private set; }
    [field: SerializeField] [Min(0)] public float m_attckRange { get; private set; }

}

public enum AttackEnemyType
{
    Range, Melee
}