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

    public Action<float> OnTakeDamage;
    public Action OnDeath;


    public float GetHealthPercent()
    {
        return (mCurrentHealth / mMaxHealth) * 100;
    }
    public float GetCurrentHealth()
    {
        return mCurrentHealth;
    }

    public float GetMaxHealth()
    {
        return mMaxHealth;
    }

    public void Start()
    {
        mCurrentHealth = mMaxHealth;
        mbIsAlive = true;
    }

    public void TakeDamage(float amount)
    {
        if (mbIsAlive == false)
        {
            return;
        }

        mCurrentHealth -= amount;
        Debug.Log(name + " has taken " + amount + " damage");


        if (mCurrentHealth < 0)
        {
            amount += mCurrentHealth;
        }
        if (OnTakeDamage != null)
        {
            OnTakeDamage(amount);
        }
        if (mCurrentHealth <= 0.0)
        {
            mbIsAlive = false;
            if (OnDeath != null)
            {
                OnDeath();
            }
        }
    }
}
