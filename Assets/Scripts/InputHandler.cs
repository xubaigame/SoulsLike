using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    private PlayerContols inputActions;
    private CameraHandler cameraHandler;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    public void Initialize()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
        cameraHandler.Initialize();
    }

    private void LateUpdate()
    {
        float delta = Time.deltaTime;
        //cameraHandler?.FollowTarget(delta);
        cameraHandler?.HandleCameraRotation(delta, cameraInput.x, cameraInput.y);
    }

    private void Awake()
    {
        if (inputActions == null)
        {
            Debug.Log("输入监听事件注册成功");
            inputActions = new PlayerContols();
            inputActions.PlayerMovement.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += context =>
            {
                Debug.Log(context.valueType);
                cameraInput = context.ReadValue<Vector2>();
                Debug.Log(cameraInput.x + " " + cameraInput.x);
            };
        }
        
        inputActions.Enable();
    }

    // private void OnEnable()
    // {
    //     if (inputActions == null)
    //     {
    //         Debug.Log("输入监听事件注册成功");
    //         inputActions = new PlayerContols();
    //         inputActions.PlayerMovement.Movement.started += context => movementInput = context.ReadValue<Vector2>();
    //         inputActions.PlayerMovement.Camera.started += context =>
    //         {
    //             cameraInput = context.ReadValue<Vector2>();
    //             Debug.Log(cameraInput.x + " " + cameraInput.x);
    //         };
    //     }
    //     
    //     inputActions.Enable();
    // }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        //MoveInput(delta);
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void OnCamera(InputValue inputValue)
    {
        cameraInput = inputValue.Get<Vector2>();
        Debug.Log(cameraInput.x + " " + cameraInput.x);
    }
}
