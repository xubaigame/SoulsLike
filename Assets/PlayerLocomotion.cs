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

    [SerializeField] private float rotationSpeed = 10;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
    }

    #region Movement

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
