using Magic.Effects;
using System.Collections.Generic;

public static class EfectsExt 
{
    public static void ApplyEffects(
        this IReadOnlyCollection<IEffect> effects,
        IEffectable effectable)
    {
        if (effects is null) return;

        foreach (IEffect effect in effects)
        {
            effect?.Apply(effectable);
        }
    }

    public static void ApplyEffects(
        this IReadOnlyCollection<IEffect> effects,
        IReadOnlyCollection<IEffectable> effectables)
    {
        if (effects is null) return;

        foreach(var effect in effects)
        {
            foreach(var effectable in effectables)
            {
                effect?.Apply(effectable);
            }
        }
    }
}
