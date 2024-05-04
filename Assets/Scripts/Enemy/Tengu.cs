using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Tengu : MonoBehaviour
{
    Rigidbody2D rb;

    public DetectionZone attackZone;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;

    GameObject playerToFollow;
    private Transform playerTransform;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;

    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }
            if (value == WalkableDirection.Right)
            {
                walkDirectionVector = Vector2.right;
            }
            else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.left;

            }
            _walkDirection = value;
        }
    }



    public float walkSpeed = 2.0f;
    public float walkStopRate = 0.05f;
    private Vector2 walkDirectionVector = Vector2.right;

    private bool _hasTarget = false;
    private bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
        set
        {

        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerToFollow.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {

        

        // if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        // {
        //     FlipDirection();
        // }

        // if (CanMove && touchingDirections.IsGrounded)
        // {
        //     rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        // }
        // else
        // {
        //     rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,0,walkStopRate), rb.velocity.y);
        // }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("WalkDirection is not set");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        Vector2 direction = playerTransform.position - transform.position;

        float dotProduct = Vector2.Dot(direction, walkDirectionVector);
        if(dotProduct < 0.0f){
            FlipDirection();
        }

        transform.Translate(direction * walkSpeed * Time.deltaTime * 0.1f);
    }
}
