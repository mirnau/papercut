using UnityEngine;

public class PaperPlaneState : BaseState
{
    public PaperPlaneState(PlayerManager PSMManager, PlayerStateMachine playerStateMachine) : base(PSMManager, playerStateMachine)
    {
    }

    public override void ApplyMovement()
    {
        base.ApplyMovement();
    }

    public override void EnterState()
    {
        playerManager.characterSprite = PlayerManager.CharacterSprite.Plane;
        playerManager.rb2D.gravityScale = 0.2f;
        base.EnterState();
    }



    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.CharacterState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.BallState);
        }
        base.Update();
    }
}
