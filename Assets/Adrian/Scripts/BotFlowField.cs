using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotFlowField : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellSize;
    public Vector2[,] flowField;
    public LayerMask wallLayerMask;
    public float wallRepulsionStrength = 10.0f;
    public float wallRepulsionRange = 5.0f;
    public GameObject bot; // Reference to the bot


    //cashed vars
    private Vector2 gridOrigin;
    private List<Collider2D> walls;
    private Collider2D wallCollider;
    private Vector2 cellCenter;
    private Vector2 directionToTarget;
    private Transform botTransform;
    private Vector2 repulsionForce = Vector2.zero;
    private Vector2 directionToWall;
    private float distanceToWall;
    private Vector2 combinedForce;
    private Collider2D npcCollider;
    private Vector2 direction;

    void Start()
    {
        flowField = new Vector2[gridSize.x, gridSize.y];
        StartCoroutine(UpdateFlowField());
    }

    IEnumerator UpdateFlowField()
    {
        while (true)
        {
            yield return new WaitForSeconds(100f);
            GenerateFlowField(bot.transform.position);

            yield return null;
        }
    }

    public void GenerateFlowField(Vector2 targetPosition)
    {
        gridOrigin = (Vector2)bot.transform.position - new Vector2(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2);

        // Create a list to store wall colliders
       walls = new List<Collider2D>();

        // Loop through all wall objects and add their colliders to the list
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            wallCollider = wall.GetComponent<Collider2D>();
            if (wallCollider != null)
            {
                walls.Add(wallCollider);
            }
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
                directionToTarget = (targetPosition - cellCenter);

                repulsionForce = Vector2.zero;
                foreach (Collider2D wall in walls)
                {
                    directionToWall = (cellCenter - (Vector2)wall.transform.position).normalized;
                    float distanceToWall = Vector2.Distance(cellCenter, wall.transform.position);
                    if (distanceToWall < wallRepulsionRange)
                    {
                        repulsionForce += directionToWall * (wallRepulsionStrength / Mathf.Pow(distanceToWall, 2));
                    }
                }

                combinedForce = directionToTarget + repulsionForce;
                float magnitude = combinedForce.magnitude;
                if (magnitude > 0f)
                {
                    flowField[x, y] = combinedForce / magnitude;
                }
                else
                {
                    flowField[x, y] = Vector2.zero;
                }
            }
        }

    }

    public Vector2 GetDirectionFromPosition(Vector2 position, LayerMask npcLayerMask)
    {
        gridOrigin = (Vector2)bot.transform.position - new Vector2(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2);
        int x = Mathf.FloorToInt((position.x - gridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((position.y - gridOrigin.y) / cellSize);

        if (x < 0 || y < 0 || x >= gridSize.x || y >= gridSize.y)
        {
            return Vector2.zero;
        }

        npcCollider = Physics2D.OverlapCircle(new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2), cellSize * 0.4f, npcLayerMask);
        if (npcCollider != null)
        {
            return Vector2.zero;
        }

        // Use the flow field to get the direction at the specified position
        return flowField[x, y];
    }
    private void OnDrawGizmos()
    {
        if (flowField != null)
        {
            gridOrigin = (Vector2)bot.transform.position - new Vector2(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2);

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
                    direction = flowField[x, y];

                    // Arrow drawing
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(cellCenter, cellCenter + direction * cellSize * 0.4f);
                    Gizmos.DrawLine(cellCenter + direction * cellSize * 0.4f, cellCenter + direction * cellSize * 0.4f - direction.Rotate(135) * cellSize * 0.2f);
                    Gizmos.DrawLine(cellCenter + direction * cellSize * 0.4f, cellCenter + direction * cellSize * 0.4f - direction.Rotate(-135) * cellSize * 0.2f);
                }
            }
        }
    }

}
