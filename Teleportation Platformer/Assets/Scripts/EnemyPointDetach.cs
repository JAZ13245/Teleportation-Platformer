using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointDetach : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
