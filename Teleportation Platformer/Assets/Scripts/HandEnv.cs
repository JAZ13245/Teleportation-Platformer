using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnv : MonoBehaviour
{
    private GameObject player;
    private bool isFacingRight = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
