using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float mMaxHealth = 100.0f;

    public float mCurrentHealth = 0;

    private bool mbIsAlive = true;
    public bool mbCanTakeDamage = true;

    public Action OnTakeDamage;
    public Action OnDeath;

    public float MCurrentHealth { get => mCurrentHealth; set => mCurrentHealth = value; }

    public void Start()
    {
        MCurrentHealth = mMaxHealth;
        mbIsAlive = true;
    }

    public void TakeDamage(float amount)
    {
        if (mbIsAlive == false)
        {
            return;
        }

        MCurrentHealth -= amount;
        OnTakeDamage();
        if (MCurrentHealth <= 0.0)
        {
            mbIsAlive = false;
            OnDeath();
        }
    }
}
