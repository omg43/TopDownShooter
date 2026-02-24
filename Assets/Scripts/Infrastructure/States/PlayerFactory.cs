using Players;
using UnityEngine;

public interface IPlayerFactorySettings
{
    public Vector3 position {  get; set; }
}

public class PlayerFactory : IPlayerFactorySettings
{
    private PlayerController m_playerPref;
    private PlayerController m_playerInstance;
    private readonly string m_path;

    Vector3 IPlayerFactorySettings.position { get; set; }

    public PlayerFactory(string mPath)
    {
        m_path = mPath;
    }  

    public PlayerController Create()
    {
        if (m_playerInstance is not null)
        {
            return m_playerInstance;
        }

        if (m_playerPref == null)
        {
            var playerPrefab = Resources.Load<GameObject>(m_path);
            m_playerPref = playerPrefab.GetComponent<PlayerController>();
        }
        
        m_playerInstance = Object.Instantiate(m_playerPref, ((IPlayerFactorySettings)this).position, Quaternion.identity);
        return m_playerInstance;
    }

    public void Release(PlayerController controller)
    {
        Object.Destroy(controller.gameObject);
        m_playerInstance = null;
    }
}

