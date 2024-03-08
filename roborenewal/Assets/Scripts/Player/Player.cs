using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static float mXSensitivityStrength = 300.0f;
    static float mYSensitivityStrength = 300.0f;

    float mXRotation = 0.0f;
    float mYRotation = 0.0f;

    public Animator mPlayerAnimator;
    public Camera mCamera;
    private PlayerInputActions inputActions;


    public Rigidbody mRigidBody;
    public CogHolder mCogHolder;

    private PlayerHand[] mHands;

    private float mSpeed = 3.0f;

    private bool isCleaning = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mHands = GetComponentsInChildren<PlayerHand>();
        StopAnimation();

        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    private void Update()
    {
        Look();
        Move();

        if (inputActions.Player.Clean.IsPressed() && !isCleaning)
        {
            StartAnimation();
        }
        else if (!inputActions.Player.Clean.IsPressed() && isCleaning) {
            StopAnimation();
        }
    }

    void StartAnimation()
    {
        isCleaning = true;
        mPlayerAnimator.speed = 1;
        mPlayerAnimator.Play("ANIM_HandSmack");

        foreach (var hand in mHands) {
            hand.SetActive(true);
        }

    }

    void StopAnimation()
    {
        isCleaning = false;
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
        float mouseX = inputActions.Player.Look.ReadValue<Vector2>().x * Time.deltaTime * mXSensitivityStrength;
        float mouseY = inputActions.Player.Look.ReadValue<Vector2>().y * Time.deltaTime * mYSensitivityStrength;

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
            Vector3 inputVector = new Vector3(inputActions.Player.Move.ReadValue<Vector2>().x, 0, inputActions.Player.Move.ReadValue<Vector2>().y);
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
