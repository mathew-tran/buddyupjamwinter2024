using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
        LayerMask layerMask = 1 << 0;
        layerMask = ~layerMask;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2.0f;
        Debug.DrawRay(transform.position, forward, Color.green);
        RaycastHit hit;

        bool bIsHit = (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f, layerMask));

        if (bIsHit)
        {
            if (hit.rigidbody)
            {
                return hit.rigidbody.gameObject;
            }
            
        }
        return null;
    }
}
