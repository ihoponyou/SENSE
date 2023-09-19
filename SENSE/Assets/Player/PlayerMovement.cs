using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float
        maxWalkSpeed,
        acceleration,
        gravity,
        jumpHeight;

    public float currentSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded;

    private void UpdateSpeed(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            if (input.magnitude > 1) input.Normalize();
            currentSpeed = Mathf.Lerp(currentSpeed, maxWalkSpeed, 0.01f);
        }
        else 
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.001f);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        UpdateSpeed(input);

        Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;
        Vector3 walkVelocity = moveDirection * currentSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * -gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(walkVelocity + velocity * Time.deltaTime);
    }
}
