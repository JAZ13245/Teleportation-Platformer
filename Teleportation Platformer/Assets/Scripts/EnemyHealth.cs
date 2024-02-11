using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (arrow != null)
        {
            health -= arrow.damage;
            if(health < 0f)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = collision.transform.position + new Vector3(0, 0, 0);
                Destroy(gameObject);
                return;
            }
        }

        PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();
        if(playerInput != null )
        {
            // decrease player health.
        }
    }
}
