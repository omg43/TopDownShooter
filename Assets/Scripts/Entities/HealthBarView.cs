using Entities;
using System.Net.NetworkInformation;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private HealthComponent m_healthComponent;

    private void OnEnable()
    {
        m_healthComponent.ValueChanged += OnValuedChanged;
    }

    private void OnDisable()
    {
        
    }

    private void OnValuedChanged()
    {

    }
}
