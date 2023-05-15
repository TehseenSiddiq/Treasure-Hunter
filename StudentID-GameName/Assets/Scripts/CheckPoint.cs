using UnityEngine.Events;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform offsetPoint;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetSpawnPoint()
    {
        gameManager.spawnPoint = offsetPoint;
    }
}
