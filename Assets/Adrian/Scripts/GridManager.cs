using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float gridSize = 10f;
    public int gridWidth = 10;
    public int gridHeight = 10;
    private List<List<List<GameObject>>> grid;

    void Start()
    {
        grid = new List<List<List<GameObject>>>();
        for (int i = 0; i < gridWidth; i++)
        {
            grid.Add(new List<List<GameObject>>());
            for (int j = 0; j < gridHeight; j++)
            {
                grid[i].Add(new List<GameObject>());
            }
        }
    }


    public void AddObject(GameObject obj)
    {
        Vector2Int cell = WorldToGrid(obj.transform.position);
        grid[cell.x][cell.y].Add(obj);
    }

    public void RemoveObject(GameObject obj)
    {
        Vector2Int cell = WorldToGrid(obj.transform.position);
        grid[cell.x][cell.y].Remove(obj);
    }

    private Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.Clamp(Mathf.FloorToInt(worldPos.x / gridSize), 0, gridWidth - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(worldPos.y / gridSize), 0, gridHeight - 1);
        return new Vector2Int(x, y);
    }

}
