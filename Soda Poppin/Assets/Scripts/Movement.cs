using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 500f;
    [SerializeField] AudioClip engineSFX;
    [SerializeField] ParticleSystem engineParticles;

    Rigidbody rb;
    AudioSource audioSource;

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
            ApplyThrust();
        }
        else
        {
            StopThrustEffects();
        }
    }

    void ApplyThrust()
    {
        Vector3 force = (Vector3.up * thrustForce * Time.deltaTime);
        rb.AddRelativeForce(force);
        //Audio
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSFX);
        }
        //Particles
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
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
    
    void StopThrustEffects()
    {
        audioSource.Stop();
        engineParticles.Stop();
    }

    public void StopEngineParticles()
    {
        engineParticles.Stop();
    }
}
