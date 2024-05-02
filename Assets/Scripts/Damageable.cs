using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    private int maxHealth = 100;
    private int currentHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    [SerializeField]
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
            }
        }
    }

    private bool _isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    [SerializeField]
    private float isInvincibilityTimer = 0.25f;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            isInvincible = true;
        }

    }
}
