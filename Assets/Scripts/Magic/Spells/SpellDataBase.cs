using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellDataBase", menuName = "ScriptableObject/Spels/SpellDataBase")]
public class SpellDataBase : ScriptableObject
{
    [SerializeField] private BaseSpellData[] m_spellData;

    public IReadOnlyList<BaseSpellData> spellDatas => m_spellData;
}
