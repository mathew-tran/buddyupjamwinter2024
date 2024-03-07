using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeployDroid : MonoBehaviour
{
    public Animator mAnimator;
    public CustomTrigger mPlayerEnterTrigger;
    public Timer mTimer;

    private CogHolder mHolderRef;
    private void Start()
    {
        mPlayerEnterTrigger.OnEnter += OnPlayerEnter;
        mPlayerEnterTrigger.OnExit += OnPlayerExit;
        mTimer.OnTimeout += OnPullTimeOut;
        OnPlayerExit();

        mHolderRef = GameManager.mInstance.mCogHolderRef;
    }

    private void OnPullTimeOut()
    {
       if (mHolderRef.IsEmpty() == false)
        {
            mHolderRef.RemoveCog();
        }
    }
    void OnPlayerEnter()
    {
        mAnimator.speed = 3;
        mAnimator.Play("ANIM_DeployOpen");
        mTimer.StartTimer();
    }

    void OnPlayerExit()
    {
        mAnimator.Play("ANIM_DeployClose");
        mTimer.StopTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == mPlayerEnterTrigger)
        {
            
        }
    }
}
