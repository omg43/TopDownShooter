using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseSpellData : ScriptableObject
{
    [SerializeField] private string m_spellName;
    [SerializeField] private ElemetType[] m_combination;
    [SerializeField] GameObject m_vissableEffect;

    //Effects
    [SerializeReferenceDropdown]
    [SerializeReference] private IEffect[] m_efects;

    public string SpellName => m_spellName;
    public IReadOnlyList<ElemetType> Comination => m_combination;    
    public GameObject VissableEffect => m_vissableEffect;


    private void OnValidate()
    {
        if(m_combination.Length > 1)
        {
            m_combination = m_combination.Take(3).ToArray();
        }
    }
}
