using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObject/Spels/AoeSpellData")]
public class AoeSpellData : BaseSpellData
{
    [SerializeField][Min(0f)] private float m_radiuse = 0f;

    public float radius => m_radiuse;
}
