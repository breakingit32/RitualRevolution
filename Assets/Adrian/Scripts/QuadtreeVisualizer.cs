using UnityEngine;

public class QuadtreeVisualizer : MonoBehaviour
{
    public PlayerMovement playerController;
    public Color quadtreeColor = Color.red;

    private void OnDrawGizmos()
    {
        if (playerController != null && playerController.quadtree != null)
        {
            DrawQuadtree(playerController.quadtree);
        }
    }

    private void DrawQuadtree(Quadtree quadtree)
    {
        if (quadtree == null) return;

        Gizmos.color = quadtreeColor;
        Gizmos.DrawWireCube(quadtree.Bounds.center, new Vector3(quadtree.Bounds.width, quadtree.Bounds.height, 0));

        if (quadtree.Nodes != null)
        {
            for (int i = 0; i < quadtree.Nodes.Length; i++)
            {
                DrawQuadtree(quadtree.Nodes[i]);
            }
        }
    }
}
