using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIToolBarHealth : MonoBehaviour
{
    private float mTotalHealth = 0.0f;

    public Image mForeGround;
    public Tool mToolRef;

    private void Start()
    {
        GameManager.GetGame().OnGameStart += Setup;
    }

    private void Setup()
    {
        GameManager.mInstance.mPlayerRef.OnToolChange += SetupTool;
        SetupTool();
    }

    private void SetupTool()
    {
        mToolRef = GameManager.mInstance.mPlayerRef.GetTool();
        if (mToolRef != null)
        {
            mToolRef.OnToolUpdate += UpdateUI;
            UpdateUI();
        }
        
    }
    void UpdateUI()
    {
        if (mToolRef != null)
        {
            Vector3 existingScale = mForeGround.transform.localScale;
            float percent = (float)mToolRef.GetDurability() / (float)mToolRef.GetMaxDurability();
            Debug.Log(percent);
            existingScale.x = Mathf.Lerp(0, 4, percent);
            mForeGround.transform.localScale = existingScale;
            Debug.Log("update");
        }

    }
}
