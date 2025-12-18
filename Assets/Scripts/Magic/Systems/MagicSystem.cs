using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public event Action <MagicState> StateChanged;
    public event Action  SpellCanceled;
    public event Action<IReadOnlyList<ElementType>> ElementChanged
    {
        add => spellPreporation.ElementsChanged += value;
        remove => spellPreporation.ElementsChanged -= value;
    }

    [SerializeField] private MagicConfig m_config;

    private MagicState m_state;
    private SpellCaster m_caster;
    private SpellPreporation m_spellPreporation;
    private Coroutine m_cooldownCoroutine;

    public MagicState state
    {
        get => m_state;
        set
        {
            if (m_state != value)
            {
                m_state = value;
                StateChanged?.Invoke(m_state);
            }
        }
    }

    private SpellPreporation spellPreporation =>
        m_spellPreporation ??= new SpellPreporation(m_config);

    private void OnEnable() =>
          spellPreporation.OverflowOccurred += CancleSpell;

    private void OnDisable() =>
          spellPreporation.OverflowOccurred -= CancleSpell;

    private void Awake()
    {
        m_caster = new SpellCaster(transform);
    }

    private void CancleSpell()
    {
        if(state is MagicState.Preporation)
        {
            spellPreporation.Clear();
            SpellCanceled?.Invoke();

            StartCooldown();
        }
    }
    
    private void StartCooldown()
    {
        if (m_cooldownCoroutine is not null)
        {
            StopCoroutine(m_cooldownCoroutine);
        }

        m_cooldownCoroutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        state = MagicState.Cooldown;
        yield return new  WaitForSeconds(m_config.camcelCooldown);
        state = MagicState.Idle;

        m_cooldownCoroutine = null;
    }

    public void AddElement(ElementType element) 
    {
        if(state is MagicState.Cooldown or MagicState.Casting)
        {
            return;
        }
        m_spellPreporation.AddElement(element);
        state = MagicState.Preporation;
    }
    public void TryCastSpell()
    {
        if (state is not MagicState.Preporation)
        {
            return;
        }

        if (spellPreporation.TryGetSpell(out var spell))
        {
            state = MagicState.Casting;

            m_caster.Cast(spell, Vector3.zero);

            spellPreporation.Clear();
            state = MagicState.Idle;
        }
        else
        {
            CancleSpell();
        }
    }
    public enum MagicState
    {
        Idle,
        Preporation,
        Cooldown,
        Casting
    }
}

