using DG.Tweening;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarker : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField][Min(0)] private float m_startSize = 0.25f;
        [SerializeField][Min(0)] private float m_finishSize = 0.5f;
        [SerializeField][Min(0.0001f)] private float m_duration = 0.5f;

        [SerializeField] private Ease m_ease = Ease.InOutSine;

        private Tweener _tween;

        public void Show(Vector3 worldPosition)
        {
            _tween?.Kill();

            gameObject.SetActive(true);

            transform.position = worldPosition;

            transform.localScale = Vector3.one * m_startSize;

            _tween = transform
                .DOScale(Vector3.one * m_finishSize, m_duration)
                .SetEase(m_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void Hide()
        {
            _tween?.Kill();
            _tween = null;

            gameObject.SetActive(false);
        }
    }
}