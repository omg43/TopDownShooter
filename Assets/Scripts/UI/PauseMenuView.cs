using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    public event Action ContinueClicked;
    public event Action MainMenuClicked;

    [SerializeField] private Button m_continue;
    [SerializeField] private Button m_mainMenu;

    private void OnEnable()
    {
        m_continue.onClick.AddListener(OnContinueClick);
        m_mainMenu.onClick.AddListener(OnMainMenuClick);
    }

    private void OnDisable()
    {
        m_continue.onClick.RemoveListener(OnContinueClick);
        m_mainMenu.onClick.RemoveListener(OnMainMenuClick);
    }

    private void OnContinueClick() =>
        ContinueClicked?.Invoke();

    private void OnMainMenuClick() =>
        MainMenuClicked?.Invoke();
}
