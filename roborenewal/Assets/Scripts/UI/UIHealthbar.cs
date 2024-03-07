using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private float mTotalHealth = 0.0f;

    public Image mForeGround;
    private void Start()
    {
        GarbageHolder holder = GameManager.mInstance.mGarbageHolderRef;
        mTotalHealth = holder.TotalHealth;
        holder.OnHealthUpdate += UpdateHealthBar;
        UpdateHealthBar(holder.GetCurrentHealth());
    }

    void UpdateHealthBar(float currentHealth)
    {
        Vector3 existingScale = mForeGround.transform.localScale;
        existingScale.x = Mathf.Lerp(0, 4, (1.0f -  (currentHealth / mTotalHealth)));
        mForeGround.transform.localScale = existingScale;
    }
}
