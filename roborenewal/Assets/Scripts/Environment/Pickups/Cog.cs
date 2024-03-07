using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour
{
    public Rigidbody mRigidBody;

    public float mMaxSpeed = 5.0f;
    void Update()
    {
        // Trying to Limit Speed
        if (mRigidBody.velocity.magnitude > mMaxSpeed)
        {
            mRigidBody.velocity = Vector3.ClampMagnitude(mRigidBody.velocity, mMaxSpeed);
        }
    }
}
