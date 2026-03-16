using Players;
using UnityEngine;

public interface IPlayerFactorySettings
{
    public Vector3 position {  get; set; }
}

public interface IPlayerFactory
{
    public PlayerController Create();

    public void Release();
}

public class PlayerFactory : IPlayerFactorySettings, IPlayerFactory
{
    private PlayerController m_playerPref;
    private PlayerController m_playerInstance;
    private readonly string m_path;

    public Vector3 position { get; set; }

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

        if (m_playerPref is null)
        {
            var playerPrefab = Resources.Load<GameObject>(m_path);
            m_playerPref = playerPrefab.GetComponent<PlayerController>();
        }

        m_playerInstance = Object.Instantiate(m_playerPref, ((IPlayerFactorySettings)this).position, Quaternion.identity);
        m_playerInstance.Initialize(Camera.main, ServiceLocator.Resolve<MouseResolver>());

        return m_playerInstance;
    }

    public void Release()
    {
        Object.Destroy(m_playerInstance.gameObject);
        m_playerInstance = null;
    }
}

