using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed;

    public float Speed
    {
        get { return _speed; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Speed must be nonnegative.");
            }

            _speed = value;
        }
    }

    void Start()
    {
        Speed = 1.0f;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        transform.position += Vector3.up * Time.deltaTime * Speed;
    }
}
