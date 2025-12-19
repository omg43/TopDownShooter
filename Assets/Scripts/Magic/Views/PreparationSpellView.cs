using Magic;
using UnityEngine;
using System.Linq;
using Magic.Systems;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace Magic.Views
{
    public sealed class PreparationSpellView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MagicSystem m_magicSystem;
        [SerializeField] private MagicConfig m_config;

        [SerializeField] private RectTransform m_elementsContainer;

        [SerializeField] private Image[] m_icons;

        [Header("Animation")]
        [SerializeField] private float m_shakeIntensity = 20f;
        [SerializeField] private float m_shakeDuration = 1f;

        private int m_activeCount;
        private Tween m_shakeTween;

        private void OnEnable()
        {
            m_magicSystem.ElementChanged += UpdateIcons;
            m_magicSystem.SpellCanceled += ShakeContainer;
        }

        private void OnDisable()
        {
            m_magicSystem.ElementChanged -= UpdateIcons;
            m_magicSystem.SpellCanceled -= ShakeContainer;
        }

        private void UpdateIcons(IReadOnlyList<ElementType> elements)
        {
            foreach (var icon in m_icons)
            {
                icon.sprite = null;
                icon.enabled = false;
            }

            if (elements is null || elements.Count is 0)
            {
                return;
            }

            for (var i = 0; i < elements.Count; i++)
            {
                var elementInfo = GetElementInfo(elements[i]);

                m_icons[i].enabled = true;
                m_icons[i].sprite = elementInfo.icon;
            }
        }

        private void ShakeContainer()
        {
            m_shakeTween?.Kill();

            var localRotation = m_elementsContainer.localRotation;

            m_shakeTween = m_elementsContainer
                .DOShakeRotation(m_shakeDuration, m_shakeIntensity)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => m_elementsContainer.localRotation = localRotation);
        }

        private ElemetData.Item GetElementInfo(ElementType type) =>
            m_config.elemetData.items.FirstOrDefault(item => item.type == type);
    }
}