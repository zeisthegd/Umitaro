using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingMovement : MovementController
{
    public SwimmingMovement() : base()
    {
    }
    public SwimmingMovement(Transform objectTransform, Rigidbody rb2d, Animator animator, MovementSettings settings) : base(objectTransform, rb2d, animator, settings)
    {
        ResetState();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        


    }

    public override void HandleAnimationTransition()
    {
    }

    public override void ResetState()
    {
        Physics.gravity = Vector3.down * settings.swimmingGravity;
    }
}
