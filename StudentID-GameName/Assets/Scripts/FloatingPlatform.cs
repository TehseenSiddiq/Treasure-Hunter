using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Transform startPos; // Starting position of the platform
    public Transform endPos; // Ending position of the platform
    public float speed = 2.0f; // Speed of the platform movement
    public float pauseTime = 1.0f; // Time to pause at each end position

    private bool movingToEnd = true; // Whether the platform is moving towards the end position
    private float currentPauseTime = 0.0f; // Time remaining for the platform to pause at each end position
    private float distanceTraveled = 0.0f; // Total distance traveled by the platform


    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        // Calculate the direction and distance to move the platform
        Vector3 direction = (endPos.position - startPos.position).normalized;
        float distance = Vector3.Distance(startPos.position, endPos.position);

        // Calculate the movement amount based on the speed and time
        float movementAmount = speed * Time.deltaTime;

        // Move the platform towards the end position
        if (movingToEnd && currentPauseTime <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, movementAmount);
            if (Vector3.Distance(transform.position, endPos.position) < 0.1f) // Check if the platform has reached the end position
            {
                movingToEnd = false;
                currentPauseTime = pauseTime;
            }
        }
        else if (!movingToEnd && currentPauseTime <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, movementAmount);
            if (Vector3.Distance(transform.position, startPos.position) < 0.1f) // Check if the platform has reached the start position
            {
                movingToEnd = true;
                currentPauseTime = pauseTime;
            }
        }

        // Pause the platform at the end position
        if (currentPauseTime > 0.0f)
        {
            currentPauseTime -= Time.deltaTime;
        }
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

}
