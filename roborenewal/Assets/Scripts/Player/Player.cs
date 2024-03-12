using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    static float mXSensitivityStrength = 15.0f;
    static float mYSensitivityStrength = 15.0f;

    float mXRotation = 0.0f;
    float mYRotation = 0.0f;

    public Animator mPlayerAnimator;
    private Tool mPlayerTool;
    public Camera mCamera;
    private PlayerInputActions mInputActions;


    public Rigidbody mRigidBody;
    public CogHolder mCogHolder;

    private float mSpeed = 3.0f;

    public GameObject mDefaultHands;

    [SerializeField] LayerMask InteractLayerMask;

    public Action OnToolChange;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        mPlayerTool = GetComponentInChildren<Tool>();
        GameManager.mInstance.mPlayerRef = this;


        mPlayerTool.StopTool();
        mPlayerTool.OnToolBroken += OnToolBroken;

        mInputActions = new PlayerInputActions();


        GameManager.GetGame().OnGameStart += OnPlayerGameStart;
        GameManager.GetGame().OnGameEnd += OnPlayerGameEnd;


    }

    public void ReplaceTool(GameObject newTool)
    {
        GameObject instance = Instantiate(newTool, transform);
        if (mPlayerTool != null)
        {
            mPlayerTool.StopTool();

            Destroy(mPlayerTool.gameObject);
            mPlayerTool = null;
        }        
        mPlayerTool = instance.GetComponent<Tool>();
        instance.GetComponent<Tool>().OnToolBroken += OnToolBroken;
        OnToolChange();
    }
    private void OnToolBroken()
    {
        Destroy(mPlayerTool.gameObject);
        return;
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
        Interact();
        Pause();



        if (mPlayerTool == null || mPlayerTool.IsSetup() == false)
        {
            return;
        }

        if (mInputActions.Player.Clean.IsPressed() )
        {
            if (mPlayerTool.CanUseTool())
            {
                mPlayerTool.StartTool();
            }
            else
            {
                mPlayerTool.StopTool();
            }
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

    public Tool GetTool()
    {
        return mPlayerTool;
    }
    void Interact()
    {
        if (mInputActions.Player.Interact.WasPressedThisFrame())
        {
            GameObject obj = GetGameObjectCollision();
            if (obj != null)
            {
                Kiosk kiosk = obj.GetComponent<Kiosk>();
                if (kiosk.CanPurchase())
                {
                    ReplaceTool(kiosk.GetTool());
                    kiosk.CompletePurchase();
                }
                Debug.Log("Attempt interact: " + obj.name);
            }
        }
    }

    void Pause()
    {
        if (mInputActions.Player.Pause.WasPressedThisFrame())
        {
            if (!PauseMenuUI.instance.IsPaused())
            {
                PauseMenuUI.instance.PauseGame();
            }
            else
            {
                PauseMenuUI.instance.ResumeGame();
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

    public GameObject GetGameObjectCollision()
    {
        Debug.Log("Attempt to interact");
        LayerMask layerMask = (1 << InteractLayerMask);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2.0f;
        Debug.DrawRay(transform.position, forward, Color.green);
        RaycastHit hit;

        bool bIsHit = (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f, layerMask));

        if (bIsHit)
        {
            if (hit.rigidbody)
            {
                return hit.rigidbody.gameObject;
            }

        }
        return null;
    }
}
