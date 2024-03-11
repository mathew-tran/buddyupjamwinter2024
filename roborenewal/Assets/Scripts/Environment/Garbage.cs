using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public Timer mTimer;
    public HealthComponent mHealthComponent;
    public Animator mAnimator;
    
    public GameObject mCogPrefab;

    private int mStep = 0;
    public int mCogsToDrop = 10;

    private void Start()
    {
        mHealthComponent.OnTakeDamage += OnGarbageTakeDamage;
        mHealthComponent.OnDeath += OnGarbageDeath;
        mTimer.OnTimeout += OnTimerTimeout;
        OnTimerTimeout();
    }

    public HealthComponent GetHealthComponent()
    {
        return mHealthComponent;
    }
    private void OnGarbageTakeDamage(float damageTaken)
    {
        mHealthComponent.mbCanTakeDamage = false;
        mTimer.StartTimer();
        mAnimator.Play("ANIM_GarbageHit");
        mAnimator.speed = 3;

        float percent = mHealthComponent.GetHealthPercent();
        if (mStep == 0)
        {
            if (percent < 90)
            {
                mStep += 1;
                int amountOfCogsToDrop = mCogsToDrop / 4;
                for(int i = 0; i < amountOfCogsToDrop; ++i)
                {
                    SpawnCog();
                }
                mCogsToDrop -= amountOfCogsToDrop;
            }
        }
        if (mStep == 1)
        {
            if (percent <= 35)
            {
                mStep += 1;
                int amountOfCogsToDrop = mCogsToDrop / 4;
                for (int i = 0; i < amountOfCogsToDrop; ++i)
                {
                    SpawnCog();
                }
                mCogsToDrop -= amountOfCogsToDrop;
            }
        }

    }

    private void SpawnCog()
    {
        GameObject obj = Instantiate(mCogPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        obj.transform.parent = GameManager.GetCogsGroup().transform;
    }
    private void OnGarbageDeath()
    {
        for(int i = 0; i < mCogsToDrop; ++i)
        {
            SpawnCog();
        }

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
        if (other.tag == "Hand")
        {
            TakeDamage(1);               
        }
    }

    public void TakeDamage(int amount)
    {
        mHealthComponent.TakeDamage(1);
    }
}
