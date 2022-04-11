using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    private PlayerContols inputActions;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerContols();
            inputActions.PlayerMovement.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += context => cameraInput = context.ReadValue<Vector2>();
        }
        
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}
