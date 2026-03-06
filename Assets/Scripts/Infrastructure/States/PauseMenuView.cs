using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    public event Action CountieClicked;
    public event Action MainMenuClicked;

    [SerializeField] private Button m_continue;
    [SerializeField] private Button m_mainMenu;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnCountieClicked() =>
        m_continue.onClick.Invoke();

    private void OnMainMenuClicked() => 
        m_mainMenu.onClick.Invoke();
}
