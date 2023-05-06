using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellSize;
    public Vector2[,] flowField;
    public float wallRepulsionStrength = 10.0f;
    public float wallRepulsionRange = 5.0f;
    public LayerMask wallLayerMask;
    private Vector2 gridOrigin;
    private Vector2 cellCenter;
    void Start()
    {
        flowField = new Vector2[gridSize.x, gridSize.y];
    }

    public void GenerateFlowField(Vector2 targetPosition)
    {
        gridOrigin = transform.position - new Vector3(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2, 0);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);

                // Check if cell is inside a tile map collider on the wall layer
                bool insideWall = Physics2D.OverlapCircle(cellCenter, cellSize / 2.0f, wallLayerMask);

                if (insideWall)
                {
                    // If cell is inside a wall, set the flow field direction to zero
                    flowField[x, y] = Vector2.zero;
                }
                else
                {
                    // Calculate direction to target
                    Vector2 directionToTarget = (targetPosition - cellCenter).normalized;

                    // Calculate repulsion force from walls
                    Vector2 repulsionForce = Vector2.zero;
                    Collider2D[] walls = Physics2D.OverlapCircleAll(cellCenter, wallRepulsionRange, wallLayerMask);
                    foreach (Collider2D wall in walls)
                    {
                        Vector2 directionToWall = (cellCenter - (Vector2)wall.transform.position).normalized;
                        float distanceToWall = Vector2.Distance(cellCenter, wall.transform.position);
                        repulsionForce += directionToWall * (wallRepulsionStrength / Mathf.Pow(distanceToWall, 2));
                    }

                    // Calculate adjusted direction to target
                    Vector2 combinedForce = directionToTarget + repulsionForce;
                    Vector2 adjustedDirection = combinedForce.normalized;

                    // Check if adjusted direction intersects a wall
                    RaycastHit2D hit = Physics2D.Raycast(cellCenter, adjustedDirection, cellSize / 2.0f, wallLayerMask);
                    if (hit.collider != null)
                    {
                        // If adjusted direction intersects a wall, adjust it to avoid the wall
                        Vector2 normal = hit.normal;
                        Vector2 reflectedDirection = Vector2.Reflect(adjustedDirection, normal);
                        adjustedDirection = reflectedDirection.normalized;
                    }

                    // Set the flow field direction
                    flowField[x, y] = adjustedDirection;
                }
            }
        }
    }










    public Vector2 GetDirectionFromPosition(Vector2 position, LayerMask npcLayerMask)
    {
        Vector2 gridOrigin = transform.position - new Vector3(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2, 0);

        int x = Mathf.FloorToInt((position.x - gridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((position.y - gridOrigin.y) / cellSize);

        if (x < 0 || y < 0 || x >= gridSize.x || y >= gridSize.y)
        {
            return Vector2.zero;
        }

        Collider2D npcCollider = Physics2D.OverlapCircle(gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2), cellSize * 0.4f, npcLayerMask);
        if (npcCollider != null)
        {
            return Vector2.zero;
        }

        return flowField[x, y];
    }

    private void OnDrawGizmos()
    {
        if (flowField != null)
        {
            Vector2 gridOrigin = transform.position - new Vector3(gridSize.x * cellSize / 2, gridSize.y * cellSize / 2, 0);

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2 cellCenter = gridOrigin + new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
                    Vector2 direction = flowField[x, y];

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
    

public static class Vector2Extension
{
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}
