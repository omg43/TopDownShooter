using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public event Action PlayClicked;
    public event Action ExitClicked;
    private Loading m_loading;
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_exitButton;
    private void Start()
    {
        m_loading = ServiceLocator.Resolve<Loading>();
    }
    private void OnEnable()
    {
        m_playButton.onClick.AddListener(OnPlayClick);
        m_exitButton.onClick.AddListener(OnExitClick);
    }
    private void OnDisable()
    {
        m_playButton.onClick.RemoveListener(OnPlayClick);
        m_exitButton.onClick.RemoveListener(OnExitClick);
    }
    private void OnPlayClick()
    {
        gameObject.SetActive(false);
        m_loading.LoadScene(GlobalConstants.Scenes.Game);
    }

    private void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}
