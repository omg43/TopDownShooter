using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public event Action <MagicState> StateChanged;
    public event Action  SpellCanceled;
    public event Action<IReadOnlyList<MagicState>> ElementChanged
    {
        add => spellPreporation.elementChanged += value;
        remove => spellPreporation.
    }

    private MagicConfig m_config;

    private MagicState m_state;
    private SpellPreporation m_spellPreporation;

    public MagicState state
    {
        get => m_state;
        set
        {
            m_state = value;
            StateChanged?.Invoke(m_state);
        }
    }

    private void OnEnable() => 
        spellPreporation.
    private void OnDisable()
    {
        
    }
    private void CancleSpell()
    {
        if(state is MagicState.Preporation)
        {
            spellPreporation.Clear();
            SpellCanceled?.Invoke();
        }
    }

    private void StartCooldown()
    {
        if(m_ is not null)
    }

    private IEnumerator CooldownRoutine()
    {
        state = MagicState.Cooldown;
        yield return new  WaitForSeconds(m_config.camcelCooldown);
        state = MagicState.Idle;

        m_cooldownCorotin = null;
    }

    private void AddElement(ElemetType element) {
        if(state is MagicState.Cooldown or MagicState.Casting)
        {
            return;
        }
        m_spellPreporation.AddElement(element);
        state = MagicState.Preporation;
    }
    private void TryCust()
    {
        if(state is not MagicState.Preporation)
        {
            return ;
        }
        if(spellPreporation.TryGetSpell(out var spell))
        {
            state = MagicState.Casting;
        }
    }
    private void RemoveElement(ElemetType type) { }

    private SpellPreporation spellPreporation =>
        m_spellPreporation ??= new SpellPreporation(m_config);

    public enum MagicState
    {
        Idle,Preporation,Cooldown, Casting
    }
}

