using System.Collections.Generic;
using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public enum WindDirection { L=0,R=1,U=2,D=3}
    public WindDirection direction;    
    public float windForce;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

    public void Start()//This is only shown for the moment to give a visual representation of the wind, will be replace with particle effects
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (direction)
        {
            case WindDirection.L:
                spriteRenderer.sprite = sprites[0];
                break;
            case WindDirection.R:
                spriteRenderer.sprite = sprites[1];
                break;
            case WindDirection.U:
                spriteRenderer.sprite = sprites[2];
                break;
            case WindDirection.D:
                spriteRenderer.sprite = sprites[3];
                break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 GetDirection()
    {
        switch (direction)
        {
            case WindDirection.L:
                return Vector2.left;
            case WindDirection.R:
                return Vector2.right;
            case WindDirection.U:
                return Vector2.up;
            case WindDirection.D: 
                return Vector2.down;
        }
        return Vector2.zero;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        if (playerManager != null /*&& playerManager.PlayerStateMachine.CurrentPlayerState == playerManager.PaperPlaneState*/)
        {
            Debug.Log("is Pushing");
            playerManager.rb2D.AddForce(GetDirection() * windForce,ForceMode2D.Force);
        }
    }
}
