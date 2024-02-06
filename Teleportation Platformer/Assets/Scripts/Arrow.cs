using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 0f;
    public Vector3 arrowDirection = Vector3.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FaceDirection();
    }
    public void OnFire()
    {
        rb.velocity = arrowDirection * arrowSpeed;
        Invoke(nameof(DestroyArrow), 10f);

    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }

    private void FaceDirection()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1), Vector3.Cross(new Vector3(0, 0, -1), new Vector3(rb.velocity.x, rb.velocity.y, 0)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<TeleportPoint>() == null)
        {
            Destroy(gameObject);
        }
    }
}
