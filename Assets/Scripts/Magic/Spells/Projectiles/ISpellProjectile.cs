using UnityEngine;
using System.Collections.Generic;

namespace Magic.Spells.Projectiles
{
    public interface ISpellProjectile
    {
        public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects);
    }
}