using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public bool mBIsActive = false;
    public void SetActive(bool bActive)
    {
        mBIsActive = bActive;
    }

    public bool IsActive()
    {
        return mBIsActive;
    }

    public GameObject GetGameObjectCollision()
    {
        //MT: I want to get the gameobject it's currently colliding with, so I can call deal damage
        return null;
    }
}
