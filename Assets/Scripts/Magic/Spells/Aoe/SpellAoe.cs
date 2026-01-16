using Magic.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Spells.Aoe
{
    public sealed class SpellAoe : MonoBehaviour, ISpellAoe
    {
        public void Initialize(Vector3 targetPosition, float radius, IReadOnlyCollection<IEffect> effects)
        {
            var colliders = Physics.OverlapSphere(targetPosition, radius, gameObject.layer);

            foreach (var collider in colliders)
            {
                var effectable = collider.GetComponent<IEffectable>();
                effects.ApplyEffects(effectable);
            }
        }
    }
}