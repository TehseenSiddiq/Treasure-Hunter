using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string code;
    public AudioClip pickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < other.GetComponent<PlayerBehviour>().items.Length; i++)
            {
                if (other.GetComponent<PlayerBehviour>().items[i].name == code)
                    other.GetComponent<PlayerBehviour>().items[i].collected = true;
            }
            Debug.Log("Item Name " + code);
            switch (code)
            {
               
                case "Heart":
                    other.GetComponent<PlayerBehviour>().health = 20;
                    break;
                case "Ammo":
                    other.GetComponent<PlayerBehviour>().maxAmmo += 10;
                    break;
            }
            AudioSource.PlayClipAtPoint(pickup, transform.position);
            Destroy(gameObject, 0.5f);
                
        }
    }
}
