using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void UpdateHealth(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            animator.SetBool("bIsDead", true);
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        // Add death logic here.
    }
}
