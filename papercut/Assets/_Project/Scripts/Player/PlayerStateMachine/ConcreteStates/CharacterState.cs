using Codice.CM.Common;
using log4net.Util;
using UnityEngine;

public class CharacterState : BaseState
{
    public CharacterState(PlayerManager PSMManager, PlayerStateMachine playerStateMachine) : base(PSMManager, playerStateMachine)
    {
    }
    public override void ApplyMovement()
    {
        base.ApplyMovement();
    }

    public override void EnterState()
    {
        playerManager.characterSprite = PlayerManager.CharacterSprite.Square;
        playerManager.rb2D.gravityScale = 1f;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.BallState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.PaperPlaneState);
        }
        base.Update();
    }
}
