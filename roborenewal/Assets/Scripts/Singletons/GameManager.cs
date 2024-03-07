using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager mInstance;
    // Start is called before the first frame update

    public GarbageHolder mGarbageHolderRef;
    public CogHolder mCogHolderRef;

    private void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
