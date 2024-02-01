using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Bow bow;

    private InputActions inputActions = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Movement.performed += OnMovementPerformed;
        inputActions.Gameplay.Movement.canceled += OnMovementCancelled;

        inputActions.Gameplay.Shoot.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Gameplay.Movement.performed -= OnMovementPerformed;
        inputActions.Gameplay.Movement.canceled -= OnMovementCancelled;

        inputActions.Gameplay.Shoot.performed -= OnShootPerformed;


    }
    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        bow.ShootArrow(worldPosition);
    }
}
