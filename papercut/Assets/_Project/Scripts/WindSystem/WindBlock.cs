using System.Collections.Generic;
using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public enum WindDirection { L=0,R=1,U=2,D=3}
    public WindDirection direction;    
    public float windForce;
    private ParticleSystem windEffect;
  
    public float minWindForce { get; } = 0;
    public float maxWindForce { get; set; } = 8f;
    public float WindForce
    {
        get => windForce;
        set
        {
            windForce = Mathf.Clamp(value, minWindForce, maxWindForce);
            SetWindEffectSpeed(windForce);
        }
    }

    public void Start()//This is only shown for the moment to give a visual representation of the wind, will be replace with particle effects
    {
        windEffect = GetComponentInChildren<ParticleSystem>();
        var newShape = windEffect.GetComponent<ParticleSystem>().shape.scale;
        switch (direction)
        {
            case WindDirection.L:
                break;
            case WindDirection.R:
                windEffect.gameObject.transform.eulerAngles = new Vector3(-180, 180, 180);
                break;
            case WindDirection.U:
                windEffect.gameObject.transform.eulerAngles = new Vector3(-180, 0, 270);
                newShape = new Vector3(5.5f, 20, 1);
                break;
            case WindDirection.D:
                windEffect.gameObject.transform.eulerAngles = new Vector3(-180, 180, -270);
                newShape = new Vector3(5.5f, 20, 1);
                break;
        }
        SetWindEffectSpeed(WindForce);
    }

    
    public void SetWindEffectSpeed(float windForce)
    {
        if(windEffect == null)return;
        var newEmission = windEffect.emission;
        newEmission.rateOverTime = windForce/3;
        
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
        if (playerManager != null && playerManager.PlayerStateMachine.CurrentPlayerState != playerManager.CharacterState)
        {
            playerManager.transform.position += (Vector3)(GetDirection() * windForce* Time.deltaTime);
        }
    }
    public void VariateWindForce(float variation)
    {
        WindForce += Random.Range(-variation, variation);
    }
}
