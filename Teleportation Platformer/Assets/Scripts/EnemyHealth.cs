using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    bool isIndestructable = false;
    [SerializeField]
    private float health = 1f;
    [SerializeField]
    private float damage = 100f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (arrow != null && !isIndestructable)
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

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if(playerHealth != null )
        {
            playerHealth.UpdateHealth(damage);
        }
    }
}
