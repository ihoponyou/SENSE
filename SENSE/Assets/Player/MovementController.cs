using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float
        moveSpeed = 4500,
        maxSpeed = 20,
        moveResistance = 0.175f,
        moveThreshold = 0.01f,
        maxSlopeAngle = 45f;

    [SerializeField] private bool
        grounded;
    
    [SerializeField] private LayerMask
        groundLayer;

    // crouch/slide
    private Vector3
        crouchScale = new Vector3(1, 0.5f, 1),
        floorNormal = Vector3.up,
        characterScale;
    [SerializeField] private float
        slideForce = 400,
        slideResistance = 0.2f;

    // jump
    [SerializeField] private bool
        canJump = true;
    [SerializeField] private float
        jumpCooldown = 0.25f,
        jumpForce = 550f;

    // input
    float x, y;
    bool jumping, walking, crouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        characterScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }

    private void UpdateInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
}
