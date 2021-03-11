using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles1;
    [SerializeField] ParticleSystem mainBoosterParticles2;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    // CACHE - e.g. references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    // STATE - private instance (member) variables

    // Start is called before the first frame update
    void Start()
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
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainBoosterParticles1.isPlaying)
            {
                mainBoosterParticles1.Play();
            }
            if (!mainBoosterParticles2.isPlaying)
            {
                mainBoosterParticles2.Play();
            }
        }
        else
        {
            mainBoosterParticles1.Stop();
            mainBoosterParticles2.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThrusterParticles.isPlaying)
            { 
                rightThrusterParticles.Play(); 
            }
        }

        

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

     void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing physics rotation se we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system applies
    }
}
