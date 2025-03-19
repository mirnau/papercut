using log4net.Util;
using UnityEngine;

public class BallState : BaseState
{
    float rotationSpeed = 300f;
    public BallState(PlayerManager PlayerManager, PlayerStateMachine playerStateMachine) : base(PlayerManager, playerStateMachine)
    {
    }

    public override void ApplyMovement()
    {
        playerManager.transform.position += new Vector3(playerManager.direction.x * playerManager.speed * Time.deltaTime, 0, 0);

    }
    public void ApplyRotation()
    {
        if (playerManager.direction != Vector2.zero)
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.CharacterState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.PaperPlaneState);
        }
        ApplyRotation();
        base.Update();
    }
}
