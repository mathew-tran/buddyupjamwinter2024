using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager mInstance;
    // Start is called before the first frame update

    public GarbageHolder mGarbageHolderRef;
    public CogHolder mCogHolderRef;
    public Player mPlayerRef;
    public GameObject mCogs;
    private void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static Game GetGame()
    {
        return mInstance.GetComponent<Game>();
    }

    public static GarbageHolder GetGarbageHolder()
    {
        return mInstance.mGarbageHolderRef;
    }

    public static CogHolder GetCogholder()
    {
        return mInstance.mCogHolderRef;
    }

    public static GameObject GetCogsGroup()
    {
        return mInstance.mCogs;
    }

}
