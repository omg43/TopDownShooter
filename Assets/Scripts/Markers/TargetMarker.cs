using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarker : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color = Color.white;
        
        [Space]
        [SerializeField] [Min(0)] private float _size = 1f;
        [SerializeField] private float _yOffset = 0.02f;

        [Header("Behaviour")]
        [SerializeField] [Min(0)] private float _lifetime = 1.25f;
        
        [Space]
        [SerializeField] private bool _isPulse = true;
       
        [Tooltip("Pulsation amplitude")]
        [SerializeField] [Range(0, 1f)] private float _pulseOffset = 0.15f;
        [SerializeField] [Min(0.01f)] private float _pulseSpeed = 6f;
        
        private float _initialSize;
        private SpriteRenderer _spriteRenderer;
        private Coroutine _showingCoroutine;
        
        public void Show(Vector3 worldPosition)
        {
            SetupView();
            
            gameObject.SetActive(true);
            _spriteRenderer.enabled = true;
            transform.position = new Vector3(worldPosition.x, worldPosition.y + _yOffset, worldPosition.z);
            
            if (_showingCoroutine is not null)
                StopCoroutine(_showingCoroutine);
            
            _showingCoroutine = StartCoroutine(Showing());
        }

        private void SetupView()
        {
            if (!_spriteRenderer)
            {
                var spriteObject = new GameObject(name: "MarkerSprite");
                
                spriteObject.transform.SetParent(transform, false);
                spriteObject.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                
                _spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
                _spriteRenderer.sortingOrder = 1000;
                _spriteRenderer.enabled = false;
                
                _spriteRenderer.sprite = _sprite;
                _spriteRenderer.color = _color;
                
                _initialSize = Mathf.Max(0.01f, _size);
                transform.localScale = Vector3.one * _initialSize;
            }
        }
        
        private IEnumerator Showing()
        {
            float time = 0f;
            
            while (time < _lifetime)
            {
                time += Time.deltaTime;
                
                if (_isPulse)
                {
                    var coefficient = 1f + Mathf.Sin(Time.time * _pulseSpeed) * _pulseOffset;
                    var scale = _initialSize * coefficient;
                    transform.localScale = Vector3.one * scale;
                }
                
                yield return null;
            }

            _spriteRenderer.enabled = false;
            transform.localScale = Vector3.one * _initialSize;
        }
    }
}