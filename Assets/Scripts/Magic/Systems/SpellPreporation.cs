using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class SpellPreporation : MonoBehaviour
{
    public event Action OverflowOccurred;
    public Action<IReadOnlyList<ElemetType>> ElementsChanged;

    private MagicConfig m_magicConfig;
    private List<ElemetType> m_elements = new();

    public SpellPreporation(MagicConfig magicConfig)
    {
        m_magicConfig = magicConfig;
    }
    public void AddElement(ElemetType elemetType)
    {
        if(m_elements.Count >= m_magicConfig.maxElements)
        {
            Clear();
            OverflowOccurred?.Invoke();
        }
        else
        {
            m_elements.Add(elemetType);
            ElementsChanged?.Invoke(m_elements);
        }
    }

    public bool TryGetSpell(out SpellDataBase spell)
    {
        spell = null;
        if(m_elements.Count is 0)
        {
            return false;
        }
        foreach (var spellData in m_magicConfig.spellDataBase.m_spellData)
        {
            //if
            //if true
            spell = m_spellData;
        }
    }
    public bool IsMatchingCombination(IReadOnlyList<ElemetType> combination)
    {
        if(combination.Count != m_elements.Count)
        {
            return false;
        }
        for(var i = 0; i < combination.Count; i++)
        {
            if (combination[i] != m_elements[i])
            {
                return false;
            }
        }
         return true;
    }
    private void Clear()
    {

    }
}
