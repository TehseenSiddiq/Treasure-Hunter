using UnityEngine.Events;
using UnityEngine;

public class ObjectCollecter : MonoBehaviour
{
    public UnityEvent Event;
    private void OnTriggerEnter(Collider other)
    {
        Event.Invoke();
       
        Destroy(gameObject);
    }
    
}
