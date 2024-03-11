using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static float mXSensitivityStrength = 15.0f;
    static float mYSensitivityStrength = 15.0f;

    float mXRotation = 0.0f;
    float mYRotation = 0.0f;

    public Animator mPlayerAnimator;
    public Tool mPlayerTool;
    public Camera mCamera;
    private PlayerInputActions mInputActions;


    public Rigidbody mRigidBody;
    public CogHolder mCogHolder;

    private PlayerHand[] mHands;

    private float mSpeed = 3.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mHands = GetComponentsInChildren<PlayerHand>();
        mPlayerTool.SetupTool();
        mPlayerTool.StopTool();
        mInputActions = new PlayerInputActions();


        GameManager.GetGame().OnGameStart += OnPlayerGameStart;
        GameManager.GetGame().OnGameEnd += OnPlayerGameEnd;
    }

    private void OnPlayerGameStart()
    {
        mInputActions.Enable();
    }
    private void OnPlayerGameEnd()
    {
        mInputActions.Disable();
    }

    private void Update()
    {
        Look();
        Move();

        if (mInputActions.Player.Clean.IsPressed() && mPlayerTool.CanUseTool())
        {
            mPlayerTool.StartTool();
        }
        else if (!mInputActions.Player.Clean.IsPressed()) {
            mPlayerTool.StopTool();
        }
    }


    void Look()
    {
        float mouseX = mInputActions.Player.Look.ReadValue<Vector2>().x * Time.deltaTime * mXSensitivityStrength;
        float mouseY = mInputActions.Player.Look.ReadValue<Vector2>().y * Time.deltaTime * mYSensitivityStrength;

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
            Vector3 inputVector = new Vector3(mInputActions.Player.Move.ReadValue<Vector2>().x, 0, mInputActions.Player.Move.ReadValue<Vector2>().y);
            if (inputVector.x != 0 || inputVector.z != 0)
            {
                inputVector = inputVector.normalized;

                Vector3 movementDirection = transform.TransformDirection(inputVector);

                Vector3 existingYVelocity = new Vector3(0, mRigidBody.velocity.y, 0);

                mRigidBody.velocity = (movementDirection * mSpeed) + existingYVelocity;
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
