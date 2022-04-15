using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;

    private Transform myTransform;
    private Vector3 cameraTransfromPosition;
    private LayerMask ignoreLayers;

    public float lookSpeed = 15f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 15f;

    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public void Initialize()
    {
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        myTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta,float mouseInputX,float mouseInputY)
    {
        //Debug.Log(mouseInputX + " " + mouseInputY);
        if (new Vector2(mouseInputX, mouseInputY).sqrMagnitude >= 0.01f)
        {
            lookAngle += mouseInputX * lookSpeed * delta;
            pivotAngle += mouseInputY * pivotSpeed * delta;
        }
        
        
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
        
        //Debug.Log(lookAngle + " " + pivotAngle);
        // Vector3 rotation = Vector3.zero;
        // rotation.y = lookAngle;
        // Quaternion targetRotation = Quaternion.Euler(rotation);
        // myTransform.rotation = targetRotation;
        //
        // rotation = Vector3.zero;
        // rotation.x = pivotAngle;
        // targetRotation = Quaternion.Euler(rotation);

        cameraTransform.rotation = Quaternion.Euler(pivotAngle, lookAngle, 0.0f);

    }

}
