using UnityEngine;

public class BaseState

{
    protected PlayerManager playerManager;
    protected PlayerStateMachine playerStateMachine;
    protected float stateGravityScale =1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public BaseState(PlayerManager PlayerManager , PlayerStateMachine playerStateMachine)
    {
        this.playerManager = PlayerManager;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Update()
    {
        ChangeState();
    }
    public virtual void PhysicsUpdate()
    {
        ApplyMovement(0);
        if(playerManager.isGrounded)
        {
            playerManager.rb2D.gravityScale = 0;
        }
        else
        {
            playerManager.rb2D.gravityScale = stateGravityScale;
        }

    }
    public virtual void EnterState() 
    {

        playerManager.transform.rotation = Quaternion.identity;
        playerManager.rb2D.gravityScale = stateGravityScale;
        playerManager.ChangeSprite();
    }

    public virtual void ExitState() { }

    public virtual void ApplyMovement(float verticalPower)
    {
        
        if (playerManager.direction.x != 0)
        {
            playerManager.transform.position += (Vector3)(playerManager.direction * playerManager.speed * Time.deltaTime);
        }
        if (playerManager.direction.y != 0 && playerManager.isGrounded)
        {
            playerManager.rb2D.AddForce(playerManager.transform.up * verticalPower,ForceMode2D.Impulse);
        }

    }
    public virtual void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.CharacterState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.BallState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerManager.PlayerStateMachine.ChangeState(playerManager.PaperPlaneState);
        }
    }

    
}
