using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public Timer mTimer;
    public HealthComponent mHealthComponent;

    private void Start()
    {
        mHealthComponent.OnTakeDamage += OnGarbageTakeDamage;
        mHealthComponent.OnDeath += OnGarbageDeath;
        mTimer.OnTimeout += OnTimerTimeout;
    }

    private void OnGarbageTakeDamage()
    {
        mHealthComponent.mbCanTakeDamage = false;
        mTimer.StartTimer();
    }

    private void OnGarbageDeath()
    {
        Destroy(this.gameObject, .2f);
    }

    private void OnTimerTimeout()
    {
        mHealthComponent.mbCanTakeDamage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag == "Hand")
        {
            if (other.gameObject.GetComponent<PlayerHand>().IsActive())
            {
                mHealthComponent.TakeDamage(1);
            }
        }
    }
}
