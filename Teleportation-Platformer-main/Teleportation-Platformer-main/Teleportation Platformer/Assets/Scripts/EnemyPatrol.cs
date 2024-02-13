using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
        {
            flip();
            currentPoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
        {
            flip();
            currentPoint = PointB.transform;
        }
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
