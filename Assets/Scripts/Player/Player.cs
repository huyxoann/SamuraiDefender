using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static AnimationStrings;
using static PlayerStats;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public HeartBar heartBar;
    public StaminaBar staminaBar;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);

        }
    }
    public bool IsJumping
    {
        get { return PlayerStats._isJumping; }
        private set
        {
            _isJumping = value;
            animator.SetBool(AnimationStrings.isJumping, value);
        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }


    public float CurrentSpeed
    {
        get
        {
            // Cant Move and Onground
            if (!CanMove && touchingDirections.IsGrounded)
            {
                return 0;
            }
            // Not moving or touching wall
            if (!IsMoving || touchingDirections.IsOnWall)
            {
                return 0;
            }
            // On Air
            if (!touchingDirections.IsGrounded)
            {
                // On Air Speed
                return airSpeed;
            }
            // On Ground and not out of stamina
            if (IsRunning && currentStamina != 0)
            {
                // OnGround Speed 
                return runningSpeed;
            }
            else
            {
                animator.SetBool(AnimationStrings.isRunning, false);
                return walkSpeed;
            }
        }
    }
    public bool _isFacingRight = true;

    // Facing, we change the localScale instead of flipX sprite
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();

        damageable.MaxHealth = PlayerStats.maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set up máu và stamina cho character
        currentHealth = damageable.MaxHealth;
        currentStamina = maxStamina;
        heartBar.SetMaxHealth(damageable.MaxHealth);
        staminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        heartBar.SetHealth(damageable.CurrentHealth);
    }

    void FixedUpdate()
    {
        // Set speed of player
        rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        ConsumeStamina();

    }
    void ConsumeStamina()
    {
        if (IsRunning)
        {
            currentStamina -= staminaRunDrainRate * Time.deltaTime;

        }
        else if (IsJumping)
        {
            currentStamina -= staminaJumpDrainRate;
            Debug.Log("Jumping: " + currentStamina);
        }
        else
        {
            // currentStamina += staminaRegenRate * Time.deltaTime;
            _ = RecoveryStaminaAsync();
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        staminaBar.SetStamina(currentStamina);
    }

    async Task RecoveryStaminaAsync()
    {
        if (currentStamina <= 10)
        {
            await Task.Delay(3000);
            currentStamina += staminaRegenRate * Time.deltaTime;

        }
        currentStamina += staminaRegenRate * Time.deltaTime;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();


        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDerection(moveInput);

        }else{
            IsMoving = false;
        }

    }

    public void SetFacingDerection(Vector2 moveInput)
    {
        if (moveInput.x < 0 && IsFacingRight)
        {
            // Face the left
            IsFacingRight = false;
        }
        else if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else { }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;

        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (currentStamina == 0)
        {
            return;
        }
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            IsJumping = true;
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        else if (context.canceled)
        {
            IsJumping = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (currentStamina <= 0)
        {
            return;
        }

        if (context.started)
        {

            animator.SetTrigger(AnimationStrings.attack);
        }
        else if (context.canceled)
        {
            currentStamina -= staminaKatanaDrainRate;
            staminaBar.SetStamina(currentStamina);
        }
    }
}
