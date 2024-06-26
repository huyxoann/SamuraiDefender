using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                IsAlive = false;
                Debug.Log("Dead");
                GameManager.IncreaseScore(10);
            }
        }
    }

    private bool _isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    [SerializeField]
    private float isInvincibilityTimer = 0.75f;

    [SerializeField]
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set to " + value);

        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > isInvincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            animator.SetTrigger(AnimationStrings.hurt);
            isInvincible = true;

            CharacterEvent.characterDamaged.Invoke(gameObject, damage);

            if(gameObject.CompareTag("Vase") || gameObject.CompareTag("Player")){
                GameManager.IncreaseScore(-1);
            }

            return true;
        }
        return false;

    }
    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            CurrentHealth += healthRestore;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            CharacterEvent.characterHealed(gameObject, healthRestore);
        }
    }
}
