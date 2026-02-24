using System;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenuView : MonoBehaviour
{
    public event Action GoToMenuClicked;
    [SerializeField] private Button m_goToMainButton;

    private void OnClicked()=> 
        GoToMenuClicked?.Invoke();

    private void OnEnable()
    {
        m_goToMainButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        m_goToMainButton.onClick.RemoveListener(OnClicked);
    }
}
