using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
    private const float
        MIN_MOUSE_SENSITIVITY = 1000f,
        MAX_MOUSE_SENSITIVITY = 1000f;

    private float _mouseSensitivity = 50f;
    public float MouseSensitivity
    {
        get 
        {
            return _mouseSensitivity;
        }
        set 
        { 
            if (_mouseSensitivity > MAX_MOUSE_SENSITIVITY || _mouseSensitivity < MIN_MOUSE_SENSITIVITY)
                throw new ArgumentOutOfRangeException($"Mouse sensitivity must be between {MIN_MOUSE_SENSITIVITY} and {MAX_MOUSE_SENSITIVITY}.");

            _mouseSensitivity = value;
        }
    }

    [SerializeField] private Transform
        subject,                // the transform to follow positionally
        character;   // the transform to rotate with the camera

    private float
        xRotation,
        mouseX,
        mouseY,
        sensMultiplier = 1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    Vector3 currentRotation;
    float desiredX = 0f, desiredY;
    void Update()
    {
        // update camera position
        transform.position = subject.position;

        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.fixedDeltaTime * sensMultiplier;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.fixedDeltaTime * sensMultiplier;

        currentRotation = transform.localRotation.eulerAngles;

        // calculate where player wants to look
        desiredY = currentRotation.y + mouseX;
        desiredX -= mouseY;
        
        // clamp x rotation to prevent looking too far up/down
        xRotation = Mathf.Clamp(desiredX, -90f, 90f);

        // rotate the camera
        transform.localRotation = Quaternion.Euler(xRotation, desiredY, 0f);
        // rotate the character
        character.localRotation = Quaternion.Euler(0, desiredY, 0);
    }
}
