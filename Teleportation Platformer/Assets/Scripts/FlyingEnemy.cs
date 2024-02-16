using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyinEnemy : MonoBehaviour
{
    public DetectionZone FireballDetectionZone;

    Animator animator;
    Rigidbody2D rb;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationScripts.hasTarget, value);
        }
    }

    public object AnimationStrings { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
}
