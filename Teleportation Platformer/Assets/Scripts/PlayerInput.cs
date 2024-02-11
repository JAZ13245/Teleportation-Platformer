using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerInput : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineRenderer controlsLineRenderer;
    public Camera cam;

    [HideInInspector]
    public float chargeAmt = 0f;
    [HideInInspector]
    public Vector3 shootDir = Vector3.zero;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Bow bow;
    [SerializeField]
    private int linePoints = 25;
    [SerializeField, Min(0.1f)]
    private float timeBetweenPoints = 0.1f;
    [SerializeField, Min(0.01f)]
    private float chargeRate = 0.1f;

    private InputActions inputActions = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator animator = null;

    private bool isFacingRight = true;

    private bool isCharging = false;
    private Vector2 mousePosOnShoot = Vector2.zero;

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

        CheckCharging();
        DrawProjectileTrace();
        DrawControlsLine();

        if(isCharging)
        {
            animator.SetBool("bIsCharging", true);
        }
        else
        {
            animator.SetBool("bIsCharging", false);
        }
    }

    private void DrawControlsLine()
    {
        if (!isCharging)
        {
            controlsLineRenderer.enabled = false;
            return;
        }

        controlsLineRenderer.enabled = true;
        controlsLineRenderer.positionCount = linePoints + 1;

        Vector2 startPos = cam.ScreenToWorldPoint(mousePosOnShoot);

        Vector2 endPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = endPos - startPos;

        int i = 0;
        controlsLineRenderer.SetPosition(i, startPos);

        for(i = 1; i <= linePoints; i++)
        {
            Vector2 pos = startPos + (i / linePoints) * direction;
            controlsLineRenderer.SetPosition(i, pos);
        }



    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        CheckMovementFlip(moveVector);
        animator.SetBool("bIsRunning", true);
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        animator.SetBool("bIsRunning", false);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        mousePosOnShoot = Mouse.current.position.ReadValue();
    }

    private void CheckMovementFlip(Vector2 moveVector)
    {
       if(moveVector.x > 0 && !isFacingRight)
       {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
       }
       else if(moveVector.x < 0 && isFacingRight)
       {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
       }
    }
    private void CheckCharging()
    {
        if(inputActions.Gameplay.Shoot.IsPressed())
        {
            Vector2 curMousePos = Mouse.current.position.ReadValue();
            
            chargeAmt = Mathf.Clamp01(chargeRate * Vector2.Distance(mousePosOnShoot, curMousePos));

            if(chargeAmt > 0.05)
                isCharging = true;
        }
        else
        {
            if(chargeAmt > 0.05)
            {
                ShootBow();
            }
            isCharging = false;
            chargeAmt = 0;
        }
    }

    private void DrawProjectileTrace()
    {
        if(!isCharging)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints/timeBetweenPoints) + 1;
        Vector3 startPos = bow._shootPoint.position;
        shootDir = GetShootDirection();
        Vector3 startVelocity = GetBowPower() * shootDir;
        int i = 0;
        lineRenderer.SetPosition(i, startPos);

        for(float t = 0; t < linePoints; t += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPos + t * startVelocity;
            point.y = startPos.y + startVelocity.y * t + (Physics.gravity.y / 2f * t * t);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPos = lineRenderer.GetPosition(i - 1);

            RaycastHit2D hit = Physics2D.Raycast(lastPos, (point - lastPos).normalized, (point - lastPos).magnitude, LayerMask.NameToLayer("Visible"));
            if(hit)
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                break;
            }

        }

        FlipSpriteIfNeeded(shootDir);
    }

    private float GetBowPower()
    {
        return Mathf.Lerp(bow.minShootSpeed, bow.maxShootSpeed, chargeAmt);
    }

    private Vector3 GetShootDirection()
    {
        //((Vector2)worldPosition - (Vector2)bow.transform.cmpPosition).normalized;

        Vector2 curMousePos = Mouse.current.position.ReadValue();

        return (mousePosOnShoot - curMousePos).normalized;
    }

    private void FlipSpriteIfNeeded(Vector3 cmpPosition)
    {

        if (cmpPosition.x < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        else if (cmpPosition.x > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
    }

    private void ShootBow()
    {
        bow.ShootArrow(shootDir, chargeAmt);
    }
}
