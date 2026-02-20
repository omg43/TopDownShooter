using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public event Action PlayClicked;
    public event Action ExitClicked;

    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_existButton;

    private void OnEnable()
    {
        m_playButton.onClick.AddListener(OnPlayClicked);
        m_existButton.onClick.AddListener(OnExitClecked);
    }

    private void OnExitClecked()
    {
        throw new NotImplementedException();
    }

    private void OnPlayClicked()
    {
        PlayClicked?.Invoke();
        SceneManager.LoadScene(GlobalConstants.Scenes.Game);
    }

    private void OnDisable()
    {
        
    }
}
