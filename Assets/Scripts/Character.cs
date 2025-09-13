using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody Rb;
    public Vector3 Movement;
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    public int maxHealth = 100;
    public int currentHealth;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void FixedUpdate()
    {
        
    }
    public virtual void OnEnable()
    {
        currentHealth = maxHealth;
        if (Rb != null) Rb.WakeUp();
    }


    public virtual void Update()
    {
        Move();
        LookAt();
    }

    public virtual void Move()
    {
        
    }

    public virtual void LookAt()
    {
        
    }
    

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public virtual bool IsDead()
    {
        return currentHealth <= 0;
    }
}