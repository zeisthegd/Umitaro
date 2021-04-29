﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
    public enum MovementState { Walk, Swim };

    [SerializeField]
    MovementSettings movementSettings;
    MovementController movementController;
    Rigidbody rb2d;
    Animator animator;



    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetAllComponents();
    }
    void Start()
    {
        InitMovementType();
    }


    void Update()
    {

        movementController.HandleInput();



    }

    void FixedUpdate()
    {

    }

    void GetAllComponents()
    {
        rb2d = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementSettings.camera = Camera.main;
    }

    void InitMovementType()
    {
        SwitchMovementType(MovementState.Swim);
    }

    void OnCollisionEnter(Collision col)
    {

    }

    void OnCollisionExit(Collision col)
    {

    }


    void SwitchMovementType(MovementState state)
    {
        switch (state)
        {
            case MovementState.Swim:
                movementController = new SwimmingMovement(this.transform, rb2d, animator, movementSettings);
                break;
            default:
                break;
        }
    }
}
