using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class CubeManager : MonoBehaviour
{
    public GameObject[] images;
    public Items[] items;
    public GameObject player;
    public UnityEvent events;
    public Transform cube;
    public AudioSource sound;
    public int score = 0;
    private void LateUpdate()
    {
        cube.Rotate(Vector3.up * (30 * Time.deltaTime));
    }
    public void UpdateImage()
    {
        StartCoroutine("CheckScore");
    }
    public IEnumerator CheckScore()
    {
        score = 0;
        for(int i =0;i < player.GetComponent<PlayerBehviour>().items.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            images[i].SetActive(player.GetComponent<PlayerBehviour>().items[i].collected);
            if (player.GetComponent<PlayerBehviour>().items[i].collected)
            {
                score++;
                sound.Play();
            }
               
        }
        if(score >= 6)
        {
            Debug.Log("Game Complete.");
        }
        yield return new WaitForSeconds(3f);
        events.Invoke();
        player.SetActive(true);
        player.GetComponent<StarterAssetsInputs>().SetCursorState(true);

    }
}
