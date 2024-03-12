using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kiosk : MonoBehaviour
{
    public GameObject mThingToSell;
    public Transform mDisplayTransform;
    private GameObject mThingToSellInstance;
    public TextMeshProUGUI mText;
    public TextMeshProUGUI mResetText;
    public Canvas mCanvas;

    private CustomTrigger mTrigger;

    private bool mBIsEnabled = false;

    public Timer mResetTimer;
    public Timer mPollTimer;

    private void Start()
    {
        mPollTimer.OnTimeout += OnPollTimerTimeout;
        mResetTimer.OnTimeout += OnResetTimerTimeout;

        mTrigger = GetComponentInChildren<CustomTrigger>();
        mTrigger.OnEnter += OnPlayerEnter;
        mTrigger.OnExit += OnPlayerExit;
        mThingToSellInstance = Instantiate(mThingToSell, mDisplayTransform);
        
        mText.text = mThingToSellInstance.GetComponent<PurchaseInfo>().mPrice.ToString();
    }

    private void OnPollTimerTimeout()
    { 
        if (mCanvas.isActiveAndEnabled == false)
        {
            return;
        }
    
        int minutes = 0;
        int seconds = 0;
        int resetTimerSeconds = (int)Mathf.Ceil(mResetTimer.GetTimeLeft());
        while(resetTimerSeconds > 0)
        {
            if (resetTimerSeconds >= 60)
            {
                resetTimerSeconds -= 60;
                minutes += 1;
            }
            else
            {
                resetTimerSeconds -= 1;
                seconds += 1;
            }
        }

        if (minutes > 0)
        {
            mResetText.text = minutes.ToString() + "m " + seconds.ToString() + "sec";
        }
        else
        {
           mResetText.text = seconds.ToString() + "sec";
        }
    }
    private void OnResetTimerTimeout()
    {
        Debug.Log("Reset kiosk");
    }
    private void OnPlayerEnter()
    {
        mCanvas.gameObject.SetActive(true);
        OnPollTimerTimeout();
        mBIsEnabled = true;
    }

    private void OnPlayerExit()
    {
        mCanvas.gameObject.SetActive(false);
        mBIsEnabled = false;
    }

    public bool CanPurchase()
    {
        if (mThingToSellInstance != null)
        {
            return true;
        }
        return false;
    }

    public void CompletePurchase()
    {
        Destroy(mThingToSellInstance.gameObject);
        mThingToSell = null;
        mText.text = "SOLD OUT";
    }

    public GameObject GetTool()
    {
        return mThingToSell;
    }
}
