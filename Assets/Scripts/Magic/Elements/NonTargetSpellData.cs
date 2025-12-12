using UnityEngine;

public class NonTargetSpellData : BaseSpellData
{
    [SerializeField][Min(0)] private float m_range;
    [SerializeField][Min(0)] private float m_duration;
    [SerializeField][Min(0)] private float m_effectIntervel;

    public float range => m_range;
    public float duration => m_duration;
    public float effectIntervel => m_effectIntervel;

}
