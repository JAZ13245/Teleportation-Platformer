using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour
{
    public Vector2 arrowVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (arrow == null) return;

        arrowVelocity = arrow.GetComponent<Rigidbody2D>().velocity;
    }
}
