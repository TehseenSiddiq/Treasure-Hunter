using UnityEngine.UI;
using UnityEngine;
using StarterAssets;

public class PicturePuzzleController : MonoBehaviour
{
    public Image[] images;
    bool allZeros = true;
    private StarterAssetsInputs starterAssetsInputs;
    [SerializeField]
    GameObject player;
    public GameObject reward;
    public GameObject ps;
    private void OnEnable()
    {
        //player = GameObject.Find("Player");
        player.SetActive(false);
    }
    public void CheckWin()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Vector3 currentRotationeluar = images[i].transform.eulerAngles;
            float zRotation = currentRotationeluar.z;
            float roundedZRotation = Mathf.Round(zRotation / 90.0f) * 90.0f;
            if (Mathf.Approximately(roundedZRotation, 360.0f))
            {
                roundedZRotation = 0.0f;              
            }
            if (roundedZRotation != 0)
            {
                allZeros = false;
                break;
            }
            
            if(i == images.Length - 1)
            {
                Debug.Log("PuzzleComplete");
                //  FindObjectOfType<StarterAssetsInputs>().SetCursorState(true);
                player.SetActive(true);
                Instantiate(reward, player.transform.localPosition + new Vector3(1f, 2f, 1f),Quaternion.identity);
                var p = Instantiate(ps, player.transform.localPosition, Quaternion.identity);
                Destroy(p, 2);
                player.GetComponent<StarterAssetsInputs>().SetCursorState(true);
                gameObject.GetComponent<FadeInOut>().Hide();
            }
        }
      
    }

}
