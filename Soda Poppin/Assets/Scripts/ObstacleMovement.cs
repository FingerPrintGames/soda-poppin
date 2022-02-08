using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor = 0f;
    [SerializeField] float cycleDuration = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        CaluclateMovement();
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    void CaluclateMovement()
    {
        if (cycleDuration <= Mathf.Epsilon) { return; }
        //The time elapsed divided by a variable sets the speed of a movement cycle
        float cycles = Time.time / cycleDuration;
        
        //A constant variable that stores tau, the number of radians in a circle
        const float tau = Mathf.PI * 2;
        
        //Returns a number between -1 and 1 based on what point in time we are at in a cycle
        float rawSinWave = Mathf.Sin(cycles * tau);

        //Sets movment factor to be a value between 0 and 1 based on what point in time we are at in a cycle
        movementFactor = (rawSinWave + 1f) / 2f;
    }
}
