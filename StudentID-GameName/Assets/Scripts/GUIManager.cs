using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Image[] hearts;
    public Color[] colors;
    public GameObject GameOverPanel;
    public GameObject LevelClearPanel;
    public TMP_Text text;

    private void Start()
    {
        InvokeRepeating("UpdateHearts", 0.5f, 0.5f);
    }

    private void UpdateHearts()
    {
   
      /*   if(GameManager.instance.lifes >= 3)
         {
            hearts[0].color = colors[0];
            hearts[1].color = colors[0];
            hearts[2].color = colors[0];
         }
        else if (GameManager.instance.lifes == 2 )
        {
            hearts[0].color = colors[0];
            hearts[1].color = colors[0];
            hearts[2].color = colors[1];
        }
        else if (GameManager.instance.lifes == 1)
        {
            hearts[0].color = colors[0];
            hearts[1].color = colors[1];
            hearts[2].color = colors[1];
        }
        else
        {
            hearts[0].color = colors[1];
            hearts[1].color = colors[1];
            hearts[2].color = colors[1];
        }*/

    }
    public void PopUpText(string text)
    {
        StartCoroutine(POPText(text));
    }
    public IEnumerator POPText(string context)
    {
        text.text = context;
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
    }
}
