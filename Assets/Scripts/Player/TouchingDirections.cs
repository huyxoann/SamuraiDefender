using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimationStrings;

public class TouchingDirections : MonoBehaviour
{

    CapsuleCollider2D capCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator animator;

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    private Vector2 wallCheck => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.IsGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.IsOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.IsOnCeiling, value);
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        capCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Ground check by using ray vector from character to ground
        // Direction: Vector2D.down
        // CastFilter: specifying which layer will be ray on

        IsGrounded = capCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = capCol.Cast(wallCheck, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

        // [Note] Use Physics2D.Raycast is better, try later
    }
}
