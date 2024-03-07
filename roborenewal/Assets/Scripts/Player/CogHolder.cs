using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogHolder : MonoBehaviour
{
    public int Limit = 100;
    public int CurrentHolding = 0;

    public void TakeCog(GameObject cog)
    {
        CurrentHolding += 1;
        cog.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(cog);
    }

    public bool IsFull()
    {
        return CurrentHolding >= Limit;
    }
}
