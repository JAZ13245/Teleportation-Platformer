using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DeathScreen;
    public float health = 100f;

    private Animator animator;
    private PlayerInput player;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerInput>();
    }
    public void UpdateHealth(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            StartCoroutine("HandleDeath");
        }
    }

    IEnumerator HandleDeath()
    {
        // Add death logic here.
        animator.SetBool("bIsDead", true);
        player.inputActions.Gameplay.Movement.Disable();
        player.inputActions.Gameplay.Shoot.Disable();
        yield return new WaitForSeconds(3f);

        DeathScreen.SetActive(true);

    }
}
