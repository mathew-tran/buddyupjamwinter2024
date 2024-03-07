using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private bool mBIsActive = false;
    public void SetActive(bool bActive)
    {
        mBIsActive = bActive;
    }
    public bool IsActive()
    {
        return mBIsActive;
    }
}
