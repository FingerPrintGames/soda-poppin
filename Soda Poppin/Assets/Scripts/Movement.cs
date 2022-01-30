using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 500f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        bool boostInput = Input.GetKey(KeyCode.Space);

        if (boostInput)
        {
            Vector3 force = (Vector3.up * thrustForce * Time.deltaTime);
            rb.AddRelativeForce(force);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    
    void ProcessRotation()
    {
        bool leftInput = Input.GetKey(KeyCode.A);
        bool rightInput = Input.GetKey(KeyCode.D);

        if (leftInput && !rightInput)
        {
            ApplyRotation(rotationForce);
        }
        if (rightInput && !leftInput)
        {
            ApplyRotation(-rotationForce);
        }
    }

    void ApplyRotation(float rotationAmountPerFrame)
    {
        rb.freezeRotation = true;
        Vector3 force = Vector3.forward * rotationAmountPerFrame * Time.deltaTime;
        transform.Rotate(force);
        rb.freezeRotation = false;
    }
}
