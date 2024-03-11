using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTool : Tool
{
    private PlayerHand[] mHands;

    private Animator mAnimator;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Garbage")
        {
            Debug.Log("Hit");
            if (other.gameObject.GetComponent<Garbage>())
            {
                other.gameObject.GetComponent<Garbage>().TakeDamage(1);
            }
        }
    }
}
