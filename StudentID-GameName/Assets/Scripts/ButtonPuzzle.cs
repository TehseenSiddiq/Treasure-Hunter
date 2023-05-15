using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using StarterAssets;

public class ButtonPuzzle : MonoBehaviour
{
    public int index;
    public Button[] toggles;
    public GameObject[] ticks;
    bool listener = false;
    public UnityEvent events;
    [SerializeField]
    GameObject player;
    public GameObject reward;
    public GameObject ps;

    private void Start()
    {
        for (int j = 0; j < toggles.Length; j++)
        {
            ticks[j].SetActive(false);
        }
    }
    private void LateUpdate()
    {
        GenerateRandom(toggles.Length, 0, toggles.Length);

    }
    public void PressBtn(int i)
    {
        int temp = Randoms[i];
        if(index == temp)
        {
            index++;
            if(index == 5)
            {
                Debug.Log("Puzzle Solved.");
                events.Invoke();
                player.SetActive(true);
                Instantiate(reward, player.transform.localPosition + new Vector3(1f, 2f, 1f), Quaternion.identity);
                var p = Instantiate(ps, player.transform.localPosition, Quaternion.identity);
                Destroy(p, 2);
                player.GetComponent<StarterAssetsInputs>().SetCursorState(true);
            }
            ticks[i].SetActive(true);
        }
        else
        {
            for (int j = 0; j < toggles.Length; j++)
            {
                ticks[j].SetActive(false);
            }
            index = 0;
        }
    }
    public List<int> Randoms;
    public void GenerateRandom(int count, int min, int max)
    {
        while(Randoms.Count < count)
        {
            Debug.Log("Count "+count);
            int a = Random.Range(min, max);
            if (Randoms.Contains(a))
                return;
            Randoms.Add(a);
            
        }
    }
}
