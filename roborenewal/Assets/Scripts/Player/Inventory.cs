using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int mTotalSize = 5;
    public List<GameObject> mInventoryObjects;

    private void Start()
    {
        mInventoryObjects = new List<GameObject>();
    }

    public bool CanAddGear()
    {
        return mInventoryObjects.Count < mTotalSize;
    }
    public void AddGear(GameObject newInventoryObject)
    {
        mInventoryObjects.Add(newInventoryObject);
    }

    public void RemoveGear(int index)
    {
        mInventoryObjects.RemoveAt(index);
    }

    public GameObject GetGear(int index)
    {
        return mInventoryObjects[index];
    }
}
