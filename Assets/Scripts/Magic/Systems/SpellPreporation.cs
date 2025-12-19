using System;
using System.Collections.Generic;

public class SpellPreporation
{
    public event Action OverflowOccurred;
    public Action<IReadOnlyList<ElementType>> ElementsChanged;

    private MagicConfig m_magicConfig;
    private List<ElementType> m_elements = new();

    public SpellPreporation(MagicConfig magicConfig)
    {
        m_magicConfig = magicConfig;
    }
    public void AddElement(ElementType elemetType)
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

    public bool TryGetSpell(out BaseSpellData spell)
    {
        spell = null;
        if (m_elements.Count is 0)
        {
            return false;
        }
        foreach (var spellData in m_magicConfig.spellDataBase.spellDatas)
        {
            if (IsMatchingCombination(spellData.Combination))
            {
                spell = spellData;
                return true;
            }
        }

        return false;
    }
    public bool IsMatchingCombination(IReadOnlyList<ElementType> combination)
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
    public void Clear()
    {
        m_elements.Clear();
        ElementsChanged?.Invoke(m_elements);
    }
}
