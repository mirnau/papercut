using log4net.Util;
using UnityEngine;

public class BallState : BaseState
{
    float rotationSpeed = 300f;
    public BallState(PlayerManager PlayerManager, PlayerStateMachine playerStateMachine) : base(PlayerManager, playerStateMachine)
    {
    }

    public override void ApplyMovement(float verticalPower)
    {
        playerManager.transform.position += new Vector3(playerManager.direction.x * playerManager.speed * Time.deltaTime, 0, 0);

    }
    public void ApplyRotation()
    {
        if (playerManager.direction.x != 0)
        {
            float rotationAmount = -playerManager.direction.x * rotationSpeed * Time.deltaTime;
            playerManager.transform.Rotate(0, 0, rotationAmount); // Rotate around Z-axis for 2D
        }
    }

    public override void EnterState()
    {
        playerManager.characterSprite = PlayerManager.CharacterSprite.Ball;
        playerManager.rb2D.gravityScale = 1f;
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
        ApplyRotation();
        base.PhysicsUpdate();
    }
}
