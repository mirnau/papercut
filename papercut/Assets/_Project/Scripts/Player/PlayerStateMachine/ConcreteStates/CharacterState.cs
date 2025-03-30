using Codice.CM.Common;
using log4net.Util;
using UnityEngine;

public class CharacterState : BaseState
{
    public CharacterState(PlayerManager PSMManager, PlayerStateMachine playerStateMachine) : base(PSMManager, playerStateMachine)
    {
    }
    public override void ApplyMovement(float verticalPower)
    {
        base.ApplyMovement(playerManager.jumpPower);
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
        base.Update();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
