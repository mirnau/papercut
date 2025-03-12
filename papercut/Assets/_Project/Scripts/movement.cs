using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float speed = 1.0f;

    private PlayerControls playerControls;

    private Vector2 direction;

    private void Awake()
    {
        playerControls = new();
        playerControls.Player.Move.performed += ctx => { direction = ctx.ReadValue<Vector2>(); };
        playerControls.Player.Move.canceled += ctx => { direction = Vector2.zero; };
    }

    private void OnEnable() => playerControls.Player.Enable();
    private void OnDisable() => playerControls.Player.Disable();

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
