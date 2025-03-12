using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float speed = 1.0f;

    private PlayerControls playerControls;
    private InputAction move;

    private Vector2 direction;

    private void Awake()
    {
        playerControls = new();
        move = playerControls.Player.Move;
        move.performed += ctx => { direction = ctx.ReadValue<Vector2>(); };
        move.canceled += ctx => { direction = Vector2.zero; };
    }

    //NOTE: Subscriptions can also be made to a method that takes InputAction.CallbackContext

    private void OnEnable() => playerControls.Player.Enable();
    private void OnDisable() => playerControls.Player.Disable();

    private void Update()
    {
        transform.position += (Vector3)(speed * Time.deltaTime * direction);
    }
}