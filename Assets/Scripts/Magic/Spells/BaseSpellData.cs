using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseSpellData : ScriptableObject
{
    [SerializeField] private string m_spellName;
    [SerializeField] private ElementType[] m_combination;
    [SerializeField] GameObject m_vissableEffect;

    //Effects
    [SerializeReferenceDropdown]
    [SerializeReference] private IEffect[] m_efects;

    public string SpellName => m_spellName;
    public IReadOnlyList<ElementType> Combination => m_combination;    
    public IReadOnlyList<IEffect> effects => m_efects;
    public GameObject VissableEffect => m_vissableEffect;


    private void OnValidate()
    {
        if(m_combination.Length > 3)
        {
            m_combination = m_combination.Take(3).ToArray();
        }
    }
}
