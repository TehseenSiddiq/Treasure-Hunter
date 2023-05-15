using DG.Tweening;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private RectTransform rectTransform;
    public PicturePuzzleController picturePuzzleController;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Rotate()
    {
        rectTransform.DORotate(new Vector3(0, 0, rectTransform.eulerAngles.z + 90),0.2f);
        this.Wait(0.3f, () => picturePuzzleController.CheckWin());
       
    }
}
