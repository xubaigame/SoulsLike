using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Transform cameraObject;
    private InputHandler inputHandler;
    private Vector3 moveDirection;

    [HideInInspector] public Transform myTransform;
    [HideInInspector] public AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;
    
    [Header("Stats")] 
    [SerializeField] private float movementSpeed = 5;

    [SerializeField] private float rotationSpeed = 12f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponent<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        inputHandler.Initialize();
        animatorHandler.Initialize();
    }

    private float time;
    private void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();

        moveDirection *= movementSpeed;
        Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        //Debug.Log(moveDirection + " " + projectVelocity);
        rigidbody.velocity = projectVelocity;

        animatorHandler.UpdateAnimatorValues(0, inputHandler.moveAmount);
        if (inputHandler.moveAmount > 0.95)
        {
            Debug.Log(time);
        }

        time += Time.deltaTime;
        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    #region Movement

    private Vector3 normalVector;
    private Vector3 targetPosition;
    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        //float moveOverride = inputHandler.moveAmount;
        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;
        
        if (targetDir == Vector3.zero)
            targetDir = myTransform.forward;
        
        float rs = rotationSpeed;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion current = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        transform.rotation = current;
    }
    

    #endregion
}
