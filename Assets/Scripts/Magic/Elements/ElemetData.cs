using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "Magic/Elements/Element")]
public sealed class ElemetData : ScriptableObject
{
    [SerializeField] private Item[] m_items;

    public IReadOnlyList<Item> items => m_items;

    [Serializable]
    public sealed class Item
    {
        [SerializeField] private string m_elementName;
        [SerializeField] private ElemetType m_type;
        [SerializeField] private Sprite m_icon;

        public String elementName => m_elementName;
        public ElemetType type => m_type;
        public Sprite icon => m_icon;

    }
}
