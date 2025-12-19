using System;
using UnityEngine;
using Magic.Spells.Aoe;
using Magic.Spells.Projectiles;
using Object = UnityEngine.Object;

public class SpellCaster
{
    private Transform m_casterTransform;

    public SpellCaster(Transform casterTransforrm)
    {
        m_casterTransform = casterTransforrm;
    }

    public void Cast(BaseSpellData spell, Vector3 worldPosition)
    {
        if (!spell)
        {
            return;
        }
        switch (spell)
        {
            case SelfSpellData selfSpell: CastSelf(selfSpell); break;
            case TargetSpellData targetSpell: CastTarget(targetSpell,worldPosition); break;
            case NonTargetSpellData nonTarget: CastNonTarget(nonTarget); break;
            case AoeSpellData aoeSpell:
                {
                    CastAoe(aoeSpell, aoeSpell.isTarget
                        ? worldPosition
                        : m_casterTransform.position);
                    break;
                }
        }
    }

    private void CastSelf(SelfSpellData selfSpell) 
    {
        if (selfSpell.VissableEffect)
        {
            Object.Instantiate(selfSpell.VissableEffect, m_casterTransform.position, Quaternion.identity);
        }

        if (m_casterTransform.TryGetComponent<IEffectable>(out var effectable))
        {
            foreach (var effect in selfSpell.effects)
            {
                effect.Apply(effectable);
            }
        }
    }
    
    private void CastTarget(TargetSpellData targetSpell, Vector3 worldPosition) 
    {
        if (!targetSpell.VissableEffect)
        {
            throw new NullReferenceException("Target spell must have visualEffect");
        }

        var projectile = Object.Instantiate(targetSpell.VissableEffect, m_casterTransform.position, Quaternion.identity);
        var spellProjectile =
            projectile.GetComponent<ISpellProjectile>() ??
            projectile.AddComponent<SpellProjectile>();

        spellProjectile.Initialize(worldPosition, targetSpell.speed, targetSpell.effects);
    }

    private void CastNonTarget(NonTargetSpellData spell) { }
    
    private void CastAoe(AoeSpellData aoeSpell, Vector3 worldPosition) 
    {
        var aoe = aoeSpell.VissableEffect
                ? Object.Instantiate(aoeSpell.VissableEffect, m_casterTransform.position, Quaternion.identity)
                : new GameObject();

        aoe.transform.position = worldPosition;

        var spellAoe =
            aoe.GetComponent<ISpellAoe>() ??
            aoe.AddComponent<SpellAoe>();

        spellAoe.Initialize(worldPosition, aoeSpell.radius, aoeSpell.effects);
    }
}

