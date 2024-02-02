using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float arrowSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * arrowSpeed;

        Invoke(nameof(DestroyArrow), 10f);
    }


    private void DestroyArrow()
    {
        Destroy(gameObject);
    }

}