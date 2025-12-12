using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] private Texture2D m_cursoreTexture;
        
        [SerializeField] [Range(0f, 100f)] private float m_speed = 5f;
        [SerializeField][Min(0)] private float m_angularSpeed;
        
        public float speed => m_speed;

        public Texture2D cursoreTexture => m_cursoreTexture;
    }
}