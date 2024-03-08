using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static float mXSensitivityStrength = 300.0f;
    static float mYSensitivityStrength = 300.0f;
    // Start is called before the first frame update

    float mXRotation = 0.0f;
    float mYRotation = 0.0f;

    public Animator mPlayerAnimator;
    public Camera mCamera;


    public Rigidbody mRigidBody;
    public CogHolder mCogHolder;

    private PlayerHand[] mHands;

    float mSpeed = 6.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mHands = GetComponentsInChildren<PlayerHand>();
        StopAnimation();

    }

    private void Update()
    {
        Look();
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            StartAnimation();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopAnimation();
        }
    }

    void StartAnimation()
    {
        mPlayerAnimator.speed = 1;
        mPlayerAnimator.Play("ANIM_HandSmack");

        foreach (var hand in mHands) {
            hand.SetActive(true);
        }

    }

    void StopAnimation()
    {
        mPlayerAnimator.speed = 0;
        mPlayerAnimator.Play("ANIM_HandSmack", 0, 0);
        mPlayerAnimator.Update(0);

        foreach (var hand in mHands)
        {
            hand.SetActive(false);
        }
    }
    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mXSensitivityStrength;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mYSensitivityStrength;

        mYRotation += mouseX;
        mXRotation -= mouseY;

        mXRotation = Mathf.Clamp(mXRotation, -15, 30f);

        transform.rotation = Quaternion.Euler(0f, mYRotation, 0f);

        mCamera.transform.rotation = Quaternion.Euler(mXRotation, mYRotation, 0f);

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
            }
            else
            {
                mRigidBody.velocity = new Vector3(0, mRigidBody.velocity.y, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cog")
        {
            if (mCogHolder.IsFull() == false)
            {
                mCogHolder.TakeCog(collision.gameObject);
            }
        }
       
    }
}
