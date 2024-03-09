using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;

public class GarbageHolder : MonoBehaviour
{
    public float TotalHealth = 0.0f;
    public float CurrentHealth = 0.0f;

    public event Action<float> OnHealthUpdate;

    public Action OnGarbageComplete;

    void Awake()
    {

        Garbage[] allGarbage = GetComponentsInChildren<Garbage>();

        foreach (var garbage in allGarbage)
        {
            HealthComponent healthComponentRef = garbage.GetHealthComponent();
            TotalHealth += healthComponentRef.GetMaxHealth();
            healthComponentRef.OnTakeDamage += OnGarbageDamageTaken;
        }

        CurrentHealth = TotalHealth;
    }

    private void OnGarbageDamageTaken(float amount)
    {
        CurrentHealth -= amount;
        OnHealthUpdate(CurrentHealth);

        if (CurrentHealth == 0)
        {
            OnGarbageComplete();
        }
    }

    public float GetMaxHealth()
    {
        return TotalHealth;
    }
    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

}
