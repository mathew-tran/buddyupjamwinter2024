using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static float mXSensitivityStrength = 200.0f;
    static float mYSensitivityStrength = 200.0f;
    // Start is called before the first frame update

    float mXRotation = 0.0f;
    float mYRotation = 0.0f;

    Rigidbody mRigidBody = null;

    float mSpeed = 3.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mRigidBody = GetComponent<Rigidbody>();
    }
    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mXSensitivityStrength;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mYSensitivityStrength;
       
        mYRotation += mouseX;
        mXRotation -= mouseY;

        mXRotation = Mathf.Clamp(mXRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, mYRotation, 0f);

        // MT: Probably want to apply mXRotation to camera instead of player. So you can look up and down.

    }

    private void Update()
    {
        Look();
        Move();
    }
    void Move()
    {
        if (mRigidBody != null)
        {
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (inputVector.x != 0 || inputVector.z != 0)
            {
                inputVector = inputVector.normalized;

                Vector3 movementDirection = transform.TransformDirection(inputVector);

                mRigidBody.velocity = movementDirection * mSpeed;
                Debug.Log("move");
            }
            else
            {
                mRigidBody.velocity = new Vector3(0, mRigidBody.velocity.y, 0);
                Debug.Log("reset");
            }



        }
    }
}
