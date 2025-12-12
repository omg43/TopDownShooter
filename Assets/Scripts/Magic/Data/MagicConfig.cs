using System.Collections.Generic;
using UnityEngine;

public sealed class MagicConfig : ScriptableObject
{
    [SerializeField] private ElemetData m_elemetData;
    [SerializeField] private SpellDataBase m_spellDataBase;

    [SerializeField] [Min(1)] private int m_maxElements;
    [SerializeField] [Min(0)] private float m_camcelCooldown;

    public ElemetData elemetData => m_elemetData;
    public SpellDataBase spellDataBase => m_spellDataBase;

    public int maxElements => m_maxElements;
    public float camcelCooldown => m_camcelCooldown;


}
