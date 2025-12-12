using UnityEngine;

public interface IEffect
{
    public void Apply(IEffectble effectble);
}
public interface IEffectble { }
