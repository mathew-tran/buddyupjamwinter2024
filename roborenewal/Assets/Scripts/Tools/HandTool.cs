using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTool : Tool
{
    private PlayerHand[] mHands;

    private Animator mAnimator;

    public override void DealDamage()
    {
        foreach(var hand in mHands)
        {
            GameObject hit = hand.GetGameObjectCollision();
            if (hit != null)
            {
                hit.GetComponent<Garbage>().TakeDamage(mDamage);
                ToolTakeHit();
            }
        }
    }

    public override void SetupTool()
    {
        mHands = GetComponentsInChildren<PlayerHand>();
        mAnimator = GetComponent<Animator>();
    }

    public override void StartTool()
    {
        mAnimator.speed = 1;
        mAnimator.SetFloat("anim_speed", mAttackRate);
        mAnimator.Play("ANIM_HandSmack");
    }


    public override void StopTool()
    {
        mAnimator.speed = 0;
        mAnimator.Play("ANIM_HandStop", 0, 0);
        mAnimator.Update(0);
    }
}
