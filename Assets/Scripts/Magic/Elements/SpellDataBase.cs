using System.Collections.Generic;
using UnityEngine;

public class SpellDataBase : ScriptableObject
{
    [SerializeField] private BaseSpellData[] m_spellData;

    public IReadOnlyList<BaseSpellData> spellDatas => m_spellData;
}
