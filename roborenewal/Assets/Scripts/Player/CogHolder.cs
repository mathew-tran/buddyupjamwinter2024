using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogHolder : MonoBehaviour
{
    public int mLimit = 100;
    public int mCurrentHolding = 0;

    public Action<int> OnUpdateCogHolder;

    public Action OnCogsComplete;

    public Timer mCheckCogTimer;

    private void Start()
    {
        mCheckCogTimer.OnTimeout += OnTimeOutCheckCogs;
    }
    
    private void OnTimeOutCheckCogs()
    {
        if (mCurrentHolding > 0)
        {
            return;
        }

        if (GameManager.GetCogsGroup().transform.childCount > 0)
        {
            return;
        }
        mCheckCogTimer.StopTimer();
        OnCogsComplete();
    }
    public void TakeCog(GameObject cog)
    {
        mCurrentHolding += 1;
        cog.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(cog);
        OnUpdateCogHolder(mCurrentHolding);
    }
    public void RemoveCog()
    {
        mCurrentHolding -= 1;
        OnUpdateCogHolder(mCurrentHolding);
        // MT: Maybe add a particle here or something..

    }
    public bool IsFull()
    {
        return mCurrentHolding >= mLimit;
    }

    public bool IsEmpty()
    {
        return mCurrentHolding <= 0;
    }
    public int GetLimit()
    {
        return mLimit;
    }

    public int GetCurrentAmount() {
        return mCurrentHolding;
    }
}
