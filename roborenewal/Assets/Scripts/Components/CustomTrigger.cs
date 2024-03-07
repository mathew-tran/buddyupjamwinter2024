using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    public string mTagToCheck;
    public Action OnEnter;
    public Action OnExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == mTagToCheck)
        {
            OnEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == mTagToCheck)
        {
            OnExit();
        }
    }
}
