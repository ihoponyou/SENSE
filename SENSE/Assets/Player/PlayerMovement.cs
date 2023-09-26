using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float
        maxWalkSpeed,
        gravity,
        jumpHeight,
        smoothness;

    [SerializeField] private Vector3 worldVelocity;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;

    private Vector3
        moveDampVelocity,
        moveVelocity = Vector3.zero,
        inputDirection;

    private Vector3 input;
    private void UpdateDirection(Vector3 input)
    {
        inputDirection = transform.TransformDirection(input);
        moveVelocity = Vector3.SmoothDamp(
            moveVelocity,
            inputDirection * maxWalkSpeed,
            ref moveDampVelocity,
            smoothness
        );
    }

    private Ray groundCheckRay;
    void Update()
    {
        groundCheckRay = new Ray(groundCheck.position, Vector3.down);
        isGrounded = Physics.Raycast(groundCheckRay, groundDistance, groundMask);

        if (isGrounded)
        {
            worldVelocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
                worldVelocity.y = Mathf.Sqrt(jumpHeight * 2f * -gravity);
        }
        else
            worldVelocity.y += gravity * Time.deltaTime;

        controller.Move(worldVelocity * Time.deltaTime);

        input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) input.Normalize();

        UpdateDirection(input);

        controller.Move(moveVelocity * Time.deltaTime);
    }
}
