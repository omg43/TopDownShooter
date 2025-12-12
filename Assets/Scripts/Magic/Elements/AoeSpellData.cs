using UnityEngine;

public class AoeSpellData : BaseSpellData
{
    [SerializeField][Min(0f)] private float m_radius = 0f;

    public float radius => m_radius;
}
