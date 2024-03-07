using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float mRate = 1.0f;
    public bool mBOnStartRunning = false;
    public bool bIsRunning = false;
    public bool bIsLooping = false;
    private float mProgress = 0.0f;

    public event Action OnTimeout;

    private void Start()
    {
        if (mBOnStartRunning)
        {
            StartTimer();
        }
    }
    public void StartTimer()
    {
        mProgress = mRate;
        bIsRunning = true;
    }

    private void Update()
    {
        if (bIsRunning)
        {
            mProgress -= Time.deltaTime;
            if (mProgress <= 0.0f)
            {
                OnTimeout();
                if (bIsLooping)
                {
                    StartTimer();
                }
                else
                {
                    bIsRunning = false;
                }
            }
        }
    }
}
