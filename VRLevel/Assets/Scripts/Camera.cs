﻿using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    [SerializeField]
    Transform target;            // The position that that camera will be following.
    float smoothing = 144f;        // The speed with which the camera will be following.
    Vector3 offset;                     // The initial offset from the target.


    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
    }
}
