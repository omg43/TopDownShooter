using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private IState m_state;
    private Dictionary<Type, IState> m_states = new();

    public void Initialize(params IState[] states)
    {
        if (m_states.Count > 0) return;

        foreach (var state in states)
        {
            m_states.Add(state.GetType(), state);   
        }
    }

    public void Update()
    {
        m_state?.Update();
    }

    public void ChangedState<T>()
        where T : IState
    {
        m_state?.Exit();
        {
            m_state = m_states[typeof(T)];
        }
        m_state.Enter();
    }
}
