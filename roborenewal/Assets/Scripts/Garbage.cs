using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public Timer mTimer;
    public HealthComponent mHealthComponent;
    public Animator mAnimator;
    private void Start()
    {
        mHealthComponent.OnTakeDamage += OnGarbageTakeDamage;
        mHealthComponent.OnDeath += OnGarbageDeath;
        mTimer.OnTimeout += OnTimerTimeout;
        OnTimerTimeout();
    }


    private void OnGarbageTakeDamage()
    {
        mHealthComponent.mbCanTakeDamage = false;
        mTimer.StartTimer();
        mAnimator.Play("ANIM_GarbageHit");
        mAnimator.speed = 3;
    }

    private void OnGarbageDeath()
    {
        Destroy(this.gameObject, .2f);
    }

    private void OnTimerTimeout()
    {
        mHealthComponent.mbCanTakeDamage = true;
        mAnimator.speed = 0;
        mAnimator.Play("ANIM_GarbageHit", 0, 0);
        mAnimator.Update(0);
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
