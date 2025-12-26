using System;
using UnityEngine;

public class EnemyStateMashine 
{
    public event Action<EnemyState, EnemyState> StateChange;

    public EnemyState currentState { get; private set; }

    public EnemyStateMashine()
    {
        currentState = EnemyState.Idle;
    }

    public void ChangeState(EnemyState nextState)
    {
        if(currentState is EnemyState.Dead || currentState == nextState)
        {
            return;
        }

        var previuseState = currentState; 
        currentState = nextState;
    }
}
