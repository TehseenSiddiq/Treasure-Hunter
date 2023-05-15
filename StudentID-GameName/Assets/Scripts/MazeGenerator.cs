using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int mazeWidth = 10;
    public int mazeHeight = 10;

    private bool[,] maze;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        maze = new bool[mazeWidth, mazeHeight];

        // Initialize maze with all walls
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = true;
                Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.identity);
            }
        }

        // Choose a random starting position
        int startX = Random.Range(0, mazeWidth);
        int startY = Random.Range(0, mazeHeight);

        // Generate maze using recursive backtracking algorithm
        GenerateMazeRecursive(startX, startY);

        // Create floors in all open spaces
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                if (!maze[x, y])
                {
                    Instantiate(floorPrefab, new Vector3(x, -0.5f, y), Quaternion.identity);
                }
            }
        }
    }

    void GenerateMazeRecursive(int x, int y)
    {
        maze[x, y] = false;

        // Randomly shuffle directions
        List<Vector2Int> directions = new List<Vector2Int>();
        directions.Add(new Vector2Int(1, 0));
        directions.Add(new Vector2Int(-1, 0));
        directions.Add(new Vector2Int(0, 1));
        directions.Add(new Vector2Int(0, -1));
        Shuffle(directions);

        // Recursive backtracking
        foreach (Vector2Int direction in directions)
        {
            int newX = x + direction.x * 2;
            int newY = y + direction.y * 2;
            if (newX >= 0 && newX < mazeWidth && newY >= 0 && newY < mazeHeight && maze[newX, newY])
            {
                maze[x + direction.x, y + direction.y] = false;
                Instantiate(floorPrefab, new Vector3(x + direction.x, -0.5f, y + direction.y), Quaternion.identity);
                Instantiate(floorPrefab, new Vector3(newX, -0.5f, newY), Quaternion.identity);
                GenerateMazeRecursive(newX, newY);
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}




