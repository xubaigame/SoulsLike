using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator animator;
    private int horizontal;
    private int vertical;
    public bool canRotate = true;
    
    public void Initialize()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement,float verticalMovement)
    {
        float h = MappingMoveRange(horizontalMovement);
        float v = MappingMoveRange(verticalMovement);
        //Debug.Log(h + " " + v);
        animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        //animator.SetFloat(vertical, v);
    }

    private float MappingMoveRange(float movement)
    {
        float value = 0;
        if (movement > 0 && movement <= 0.55f)
            value = 0.55f;
        else if (movement > 0.55f)
            value = 1f;
        else if (movement < 0 && movement >= -0.55f)
            value = -0.55f;
        else if (movement < -0.55f)
            value = -1f;
        else
            value = 0;
        return value;
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotate()
    {
        canRotate = false;
    }
}
