using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Storagebar : MonoBehaviour
{
    private int mMaxAmount;
    public Image mForeGround;
    private void Start()
    {
        CogHolder holder = GameManager.mInstance.mCogHolderRef;
        mMaxAmount = holder.GetLimit();
        holder.OnUpdateCogHolder += UpdateStorageBar;
        UpdateStorageBar(holder.GetCurrentAmount());
    }

    void UpdateStorageBar(int currentAmount)
    {
        Vector3 existingScale = mForeGround.transform.localScale;
        existingScale.x = Mathf.Lerp(0, 4, (float)currentAmount / mMaxAmount);
        mForeGround.transform.localScale = existingScale;
    }
}
