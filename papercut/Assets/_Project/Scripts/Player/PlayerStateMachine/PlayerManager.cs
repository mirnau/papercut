using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public enum CharacterSprite
    {
        Square,
        Ball,
        Plane
    };
    //StateMachine
    [HideInInspector] public PlayerStateMachine PlayerStateMachine;

    [HideInInspector] public CharacterState CharacterState;
    [HideInInspector] public BallState BallState;
    [HideInInspector] public PaperPlaneState PaperPlaneState;
    [HideInInspector] public CharacterSprite characterSprite;
    [HideInInspector] private SpriteRenderer spriteRenderer;
    [HideInInspector] public Rigidbody2D rb2D;

    //Chaning States
    public List<Sprite> playerSprites;
    private PolygonCollider2D polygonCollider;

    //Movement
    public float speed = 1.0f;
    private PlayerControls playerControls;
    public Vector2 direction;

    private void OnEnable() => playerControls.Player.Enable();
    private void OnDisable() => playerControls.Player.Disable();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        PlayerStateMachine = new PlayerStateMachine();

        CharacterState = new CharacterState(this, PlayerStateMachine);
        BallState = new BallState(this, PlayerStateMachine);
        PaperPlaneState = new PaperPlaneState(this,PlayerStateMachine);

        playerControls = new();
        playerControls.Player.Move.performed += ctx => { direction = ctx.ReadValue<Vector2>(); };
        playerControls.Player.Move.canceled += ctx => { direction = Vector2.zero; };
    }
    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        PlayerStateMachine.Initialize(CharacterState);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStateMachine.CurrentPlayerState.Update();
    }

    //Denna delen av koden kommer att ändras när vi har riktiga sprites
    public void ChangeSprite()
    {
        switch (characterSprite)
        {
            case CharacterSprite.Square:
                spriteRenderer.sprite = playerSprites[0];
                break;
            case CharacterSprite.Ball:
                spriteRenderer.sprite = playerSprites[1];
                break;
            case CharacterSprite.Plane:
                spriteRenderer.sprite = playerSprites[2];
                break;
        }
        UpdateCollider();
    }
    void UpdateCollider()
    {
        polygonCollider.pathCount = 0; // Clear old collider shape
        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path); // Fetch new shape
        polygonCollider.SetPath(0, path);
    }
}
