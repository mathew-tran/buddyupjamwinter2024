using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public abstract class Tool : MonoBehaviour
{
    public int mDurability = 10;

    public float mAttackRate = 1.5f;
    public float mDamage = 1;

    public Action OnToolBroken;
    public bool bInfiniteDurability = false;

    private void Start()
    {
        SetupTool();
        StopTool();
    }
    public abstract void SetupTool();
    public abstract void StartTool();
    public abstract void StopTool();

    public bool CanUseTool()
    {
        return mDurability > 0;
    }
    public void ToolTakeHit()
    {
        if (bInfiniteDurability)
        {
            return;
        }
        mDurability -= 1;
        if (mDurability <= 0)
        {
            OnToolBroken();
        }
    }

    public abstract void DealDamage();
}


