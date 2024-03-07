using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogHolder : MonoBehaviour
{
    public int mLimit = 100;
    public int mCurrentHolding = 0;

    public Action<int> OnUpdateCogHolder;
    public void TakeCog(GameObject cog)
    {
        mCurrentHolding += 1;
        cog.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(cog);
        OnUpdateCogHolder(mCurrentHolding);
    }

    public bool IsFull()
    {
        return mCurrentHolding >= mLimit;
    }

    public int GetLimit()
    {
        return mLimit;
    }

    public int GetCurrentAmount() {
        return mCurrentHolding;
    }
}
