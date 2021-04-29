using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController
{
    protected Animator animator;
    protected Transform objectTrf;
    protected Rigidbody rb2d;
    protected MovementSettings settings;

    public MovementController() { }
    public MovementController(Transform objectTransform, Rigidbody rb2d, Animator animator, MovementSettings settings)
    {
        this.objectTrf = objectTransform;
        this.rb2d = rb2d;
        this.settings = settings;
        this.animator = animator;
        ResetState();
    }

    public virtual void HandleInput()
    {
        ApplySwimMovement();
        Levitate();
        Descend();
        HandleAnimationTransition();
    }

    public abstract void HandleAnimationTransition();

    private void ApplySwimMovement()
    {
        MovePlayerOnXandZ();
    }



    private void MovePlayerOnXandZ()
    {
        float horiInp = Input.GetAxisRaw("Horizontal");
        float vertInp = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horiInp, 0F, vertInp).normalized;

        animator.SetBool("isSwimming", direction.magnitude >= 0.1F);
        Debug.DrawRay(settings.camera.transform.position, settings.camera.transform.forward * 100F, Color.blue);

        if (direction.magnitude >= 0.1F)
        {
            float horiAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + settings.camera.transform.eulerAngles.y;
            float angleY = Mathf.SmoothDampAngle(objectTrf.eulerAngles.y, horiAngle, ref settings.turnSmoothVelocity, settings.turnSmooth);
            objectTrf.rotation = Quaternion.Euler(0F, angleY, 0F);

            direction = Quaternion.Euler(0F, horiAngle, 0F) * Vector3.forward + (settings.camera.transform.forward.y * Vector3.up);
        
            float speedMultiplier = (Input.GetButton("Sprint")) ? settings.sprintMultiplier : 1;
            rb2d.AddForce(direction * settings.moveSpeed * speedMultiplier * Time.deltaTime, ForceMode.Acceleration);
        }
    }


    private void Levitate()
    {
        if (Input.GetButton("Levitate"))
        {
            Vector3 jumpForce = Vector3.up * settings.jumpHeight;
            rb2d.AddForce(jumpForce, ForceMode.Acceleration);
        }
    }
    private void Descend()
    {
        if (Input.GetButton("Descend"))
        {
            Vector3 jumpForce = Vector3.down * settings.jumpHeight;
            rb2d.AddForce(jumpForce, ForceMode.Acceleration);
        }
    }

    public abstract void ResetState();
}
