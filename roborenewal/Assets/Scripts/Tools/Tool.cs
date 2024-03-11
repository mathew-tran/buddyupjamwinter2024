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

    public Action OnToolBroken;

    public abstract void SetupTool();
    public abstract void StartTool();
    public abstract void StopTool();

    public bool CanUseTool()
    {
        return mDurability > 0;
    }
    public void ToolTakeHit()
    {
        mDurability -= 1;
        if (mDurability <= 0)
        {
            OnToolBroken();
        }
    }

    public void DealDamage()
    {

    }
}


