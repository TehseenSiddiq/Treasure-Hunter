using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePuzzleController : MonoBehaviour
{
    public Vector3[] correctPositions;
    public Vector3 offset;

    void Start()
    {
        Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = correctPositions[i];
            Debug.Log(transform.GetChild(i).position == correctPositions[i]);
        }
    }
    void Update()
    {
        bool solved = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            float distance = Vector2.Distance(transform.GetChild(i).position, correctPositions[i]);
            if (distance > 0.1f)
            {
                solved = false;
                break;
            }
        }
        if (solved)
        {
            Debug.Log("Puzzle solved!");
        }
    }
    void OnMouseDown()
    {
        // Calculate the offset between the mouse position and the puzzle piece position.
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        // Move the puzzle piece to the current mouse position.
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        // Snap the puzzle piece to the nearest correct position.
        float closestDistance = float.MaxValue;
        Vector2 closestPosition = Vector2.zero;
        for (int i = 0; i < correctPositions.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, correctPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = correctPositions[i];
            }
        }
        transform.position = closestPosition;
    }
}
