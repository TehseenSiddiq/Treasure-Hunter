using UnityEngine.SceneManagement;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Transform spawnPoint;
    public Transform Player;
    public bool inPuzzle;
    public GameObject GameCompleteScreen, LoseScreen;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ResetPlayer();

        Player.GetComponent<StarterAssetsInputs>().SetCursorState(false);
        Player.gameObject.SetActive(false);
    }
    public void StartGame()
    {

        Player.GetComponent<StarterAssetsInputs>().SetCursorState(true);
        Player.gameObject.SetActive(true);
    }
    public void ResetPlayer()
    {
        Debug.Log("Reseting Player");
        Player.gameObject.SetActive(false);
        Player.position = spawnPoint.position;
        Player.gameObject.SetActive(true);
    }
    public void GameComplete()
    {
        FindObjectOfType<GUILookAtCamera>().PuzzleStart();
        GameCompleteScreen.SetActive(true);
    }
    public void GameLost()
    {
        FindObjectOfType<GUILookAtCamera>().PuzzleStart();
        LoseScreen.SetActive(true);
    }
    public void ToggleInPuzzle(bool input)
    {
        inPuzzle = input;
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
