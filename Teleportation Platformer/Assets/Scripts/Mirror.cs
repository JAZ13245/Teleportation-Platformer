using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private MirrorTrigger trigger;
    [SerializeField] float exitVelocityIncrease = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (arrow == null) return;

        ContactPoint2D contact = collision.GetContact(0);
        Rigidbody2D rb = arrow.gameObject.GetComponent<Rigidbody2D>();
        Vector2 incoming = trigger.arrowVelocity;
        Vector2 normal = -contact.normal;
        Vector2 outgoing = Vector2.Reflect(incoming, normal);

        Debug.DrawLine(contact.point, contact.point - incoming, Color.blue, 10f);
        Debug.DrawLine(contact.point, contact.point + normal, Color.yellow, 10f);
        Debug.DrawLine(contact.point, contact.point + outgoing, Color.red, 10f);

        arrow.gameObject.transform.position = contact.point + (0.1f *outgoing);
        rb.velocity = outgoing  * exitVelocityIncrease;
    }
}
