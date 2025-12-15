using UnityEngine;

public interface IEffect
{
    public void Apply(IEffectable effectble);
}
public interface IEffectable { }
