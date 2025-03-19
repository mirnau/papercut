using UnityEngine;

public class BaseState

{
    protected PlayerManager playerManager;
    protected PlayerStateMachine playerStateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public BaseState(PlayerManager PlayerManager , PlayerStateMachine playerStateMachine)
    {
        this.playerManager = PlayerManager;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Update()
    {
        ApplyMovement();
    }
    public virtual void EnterState() 
    {

        playerManager.transform.rotation = Quaternion.identity;
        playerManager.ChangeSprite();
    }

    public virtual void ExitState() { }

    public virtual void ApplyMovement()
    {
        playerManager.transform.position += (Vector3)(playerManager.direction * playerManager.speed * Time.deltaTime);
    }

    
}
