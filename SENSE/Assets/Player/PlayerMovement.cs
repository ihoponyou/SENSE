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
        acceleration,
        gravity,
        jumpHeight;

    public float currentSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private Vector3 worldVelocity;
    [SerializeField] private bool isGrounded;

    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float snappiness;

    private void UpdateSpeed(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxWalkSpeed);
        }
        else 
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.deltaTime, 0);
    }

    private Vector3 inputDirection;
    private void UpdateDirection(Vector2 input)
    {
        inputDirection = transform.forward * input.y + transform.right * input.x;
        moveDirection = Vector3.Lerp(moveDirection, (input.magnitude > 0) ? inputDirection : Vector3.zero, snappiness);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && worldVelocity.y < 0)
            worldVelocity.y = -2f;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        UpdateSpeed(input);
        UpdateDirection(input);

        Vector3 inputVelocity = moveDirection.normalized * currentSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            worldVelocity.y = Mathf.Sqrt(jumpHeight * 2f * -gravity);
        }

        worldVelocity.y += gravity * Time.deltaTime;
        controller.Move(inputVelocity + worldVelocity * Time.deltaTime);
    }
}
