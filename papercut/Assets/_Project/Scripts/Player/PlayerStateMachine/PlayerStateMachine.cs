using UnityEngine;

public class PlayerStateMachine 
{
    public BaseState CurrentPlayerState { get;  set; }

    public void Initialize(BaseState startingState)
    {
        CurrentPlayerState = startingState;
        CurrentPlayerState.EnterState();
    }

    public void ChangeState(BaseState newState)
    {
        CurrentPlayerState.ExitState();
        CurrentPlayerState = newState;
        CurrentPlayerState.EnterState();
    }

}
