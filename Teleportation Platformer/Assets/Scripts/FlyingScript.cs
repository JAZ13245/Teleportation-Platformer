using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingScript : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        currentPoint = PointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, -speed);
        }
        else
        {
            rb.velocity = new Vector2(-speed, speed);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
        {
            currentPoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
        {
            currentPoint = PointD.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointD.transform)
        {
            currentPoint = PointC.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointC.transform)
        {
            currentPoint = PointB.transform;
        }
    }
}
