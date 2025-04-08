using UnityEngine;

public class PaperPlaneState : BaseState
{
    public PaperPlaneState(PlayerManager PSMManager, PlayerStateMachine playerStateMachine) : base(PSMManager, playerStateMachine)
    {
        stateGravityScale = 0.4f;
    }

    public override void ApplyMovement(float verticalPower)
    {
        base.ApplyMovement(playerManager.flightPower);
    }

    public override void EnterState()
    {
        playerManager.characterSprite = PlayerManager.CharacterSprite.Plane;
        
        base.EnterState();
    }



    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        ChangeState();
        base.Update();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
