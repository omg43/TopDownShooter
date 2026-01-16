using Magic.Effects;
using Magic.Spells.Aoe;
using Magic.Spells.Data;
using Magic.Spells.Projectiles;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Magic.Systems
{
    public sealed class SpellCaster
    {
        private readonly Transform m_casterTransform;
        private readonly bool m_isSingelSpell = false;
        private ObjectPool<GameObject> m_visualEffectPool;

        public SpellCaster(Transform casterTransform, bool singelSpell = false)
        {
            m_casterTransform = casterTransform;
            m_isSingelSpell = singelSpell;
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
                case TargetSpellData targetSpell: CastTarget(targetSpell, worldPosition); break;
                case NonTargetSpellData nonTargetSpell: CastNonTarget(nonTargetSpell); break;
                case AoeSpellData aoeSpell:
                {
                    if (aoeSpell.isTarget)
                    {
                        CastAoe(aoeSpell, worldPosition);
                    }
                    else
                    {
                        CastAoe(aoeSpell, m_casterTransform.position);
                    }
                    break;
                }
            }
        }

        private void CastSelf(SelfSpellData selfSpell)
        {
            if (selfSpell.visualEffect)
            {
                var visualEffect = Object.Instantiate(selfSpell.visualEffect);
                SetLayer(visualEffect);

            }

            var effectables = m_casterTransform.GetComponents<IEffectable>();
            selfSpell.effects.ApplyEffects(effectables);
        }
        
        private void CastTarget(TargetSpellData targetSpell, Vector3 worldPosition)
        {
            if (!targetSpell.visualEffect)
            {
                throw new NullReferenceException("Target spell must have visualEffect");
            }

            var projectile = Object.Instantiate(targetSpell.visualEffect, m_casterTransform.position, Quaternion.identity);
            SetLayer(projectile);
            var spellProjectile =
                projectile.GetComponent<ISpellProjectile>() ??
                projectile.AddComponent<SpellProjectile>();

            spellProjectile.Initialize(worldPosition, targetSpell.speed, targetSpell.effects);
        }

        private void CastNonTarget(NonTargetSpellData nonTargetSpell){}

        private void CastAoe(AoeSpellData spell, Vector3 worldPosition)
        {
            GameObject aoe;

            if (m_isSingelSpell)
            {
                m_visualEffectPool ??= new ObjectPool<GameObject>(
                    createFunc: Create,
                    actionOnGet: gm => gm.SetActive(true),
                    actionOnRelease: gm => gm.SetActive(false),
                    actionOnDestroy: Object.Destroy);

                aoe = m_visualEffectPool.Get();
            }
            else
            {
                aoe = Create();
            }

            SetLayer(aoe);
            aoe.transform.position = worldPosition;


            var spellAoe =
                aoe.GetComponent<ISpellAoe>() ??
                aoe.AddComponent<SpellAoe>();

            spellAoe.Initialize(worldPosition, spell.radius, spell.effects);

            if (m_isSingelSpell)
            {
                m_visualEffectPool.Release(aoe);
            }
            else
            {
                Object.Destroy(aoe);
            }
            return;


            GameObject Create()
            {
                return spell.visualEffect
                    ? Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity)
                : new GameObject();
            }
        }
        private void SetLayer(GameObject visualEffect) =>
        visualEffect.layer = m_casterTransform.gameObject.layer;
    }
}

