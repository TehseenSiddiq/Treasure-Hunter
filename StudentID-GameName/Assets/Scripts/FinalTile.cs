using UnityEngine.Events;
using UnityEngine;

public class FinalTile : MonoBehaviour
{
    public UnityEvent events;
    private void OnTriggerEnter(Collider other)
    {
           events.Invoke();
    }
}
