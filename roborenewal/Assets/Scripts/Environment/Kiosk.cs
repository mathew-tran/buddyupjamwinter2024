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
    public Canvas mCanvas;

    private CustomTrigger mTrigger;

    private bool mBIsEnabled = false;


    private void Start()
    {
        mTrigger = GetComponentInChildren<CustomTrigger>();
        mTrigger.OnEnter += OnPlayerEnter;
        mTrigger.OnExit += OnPlayerExit;
        mThingToSellInstance = Instantiate(mThingToSell, mDisplayTransform);
        
        mText.text = mThingToSellInstance.GetComponent<PurchaseInfo>().mPrice.ToString();
    }

    private void OnPlayerEnter()
    {
        mCanvas.gameObject.SetActive(true);
        mBIsEnabled = true;
    }

    private void OnPlayerExit()
    {
        mCanvas.gameObject.SetActive(false);
        mBIsEnabled = false;
    }
}
