using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField]
    private float _teleportYOffest;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Arrow>() != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = collision.transform.position + new Vector3(0, _teleportYOffest, 0);
            Destroy(gameObject);
        }
    }
}
