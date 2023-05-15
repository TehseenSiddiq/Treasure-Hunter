using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUILookAtCamera : MonoBehaviour
{
    public Transform player;
    public GameObject canvas;
    public float range = 10;
    public UnityEvent events;
    public bool complete;

    public void ToggleComplete(bool complete)
    {
        this.complete = complete;
    }
    private void LateUpdate()
    {
        if (complete)
        {
            canvas.SetActive(false);
            return;
        }
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            canvas.SetActive(true);
            if (player.GetComponent<StarterAssetsInputs>().interact)
            {
                Debug.Log("Check Point Set");
                // gameManager.spawnPoint = offsetPoint;
                events.Invoke();
                player.GetComponent<StarterAssetsInputs>().interact = false;
            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }
    public void PuzzleStart()
    {
        FindObjectOfType<StarterAssetsInputs>().SetCursorState(false);
        player.gameObject.SetActive(false);
    }
}
