using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnv : MonoBehaviour
{
    private GameObject player;
    private bool isFacingRight = true;
    private float randomStart;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        randomStart = Random.Range(.5f, 1.5f);
        this.transform.GetComponentInChildren<Animator>().speed = randomStart;
    }

    private void Update()
    {
        if (isFacingRight && player.transform.position.x < transform.position.x) 
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        else if(!isFacingRight && player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
    }
}
