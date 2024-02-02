using System;
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
    private Animator animator = null;
    private bool isFacingRight = true;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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

    private void Update()
    {
        CheckIfNeedFlip();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        animator.SetBool("bIsRunning", true);
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        animator.SetBool("bIsRunning", false);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        bow.ShootArrow(worldPosition);
    }
    private void CheckIfNeedFlip()
    {
        if (Mouse.current == null) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        if(worldPosition.x < transform.position.x && isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        else if(worldPosition.x > transform.position.x && !isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
    }
}
