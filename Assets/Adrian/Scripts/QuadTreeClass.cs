using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
    private int maxObjects = 10;
    private int maxLevels = 5;

    private int level;
    private List<GameObject> objects;
    private Rect bounds;
    private Quadtree[] nodes;

    public Quadtree(int level, Rect bounds)
    {
        this.level = level;
        this.bounds = bounds;
        objects = new List<GameObject>();
        nodes = new Quadtree[4];
    }
    public Quadtree(int level, Rect bounds, int maxLevels)
    {
        this.level = level;
        this.bounds = bounds;
        this.maxLevels = maxLevels;
        objects = new List<GameObject>();
        nodes = new Quadtree[4];
    }

    public Quadtree[] Nodes
    {
        get { return nodes; }
    }
    public Rect Bounds
    {
        get { return bounds; }
    }
    public Quadtree(int maxObjects, int maxLevels, Rect bounds)
    {
        this.maxObjects = maxObjects;
        this.maxLevels = maxLevels;
        this.bounds = bounds;
    }


    public void Clear()
    {
        objects.Clear();

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
            {
                nodes[i].Clear();
                nodes[i] = null;
            }
        }
    }

    private void Split()
    {
        float subWidth = bounds.width / 2;
        float subHeight = bounds.height / 2;
        float x = bounds.x;
        float y = bounds.y;

        nodes[0] = new Quadtree(level + 1, new Rect(x + subWidth, y, subWidth, subHeight));
        nodes[1] = new Quadtree(level + 1, new Rect(x, y, subWidth, subHeight));
        nodes[2] = new Quadtree(level + 1, new Rect(x, y + subHeight, subWidth, subHeight));
        nodes[3] = new Quadtree(level + 1, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
    }

    private int GetIndex(GameObject gameObject)
    {
        int index = -1;
        double verticalMidpoint = bounds.x + (bounds.width / 2);
        double horizontalMidpoint = bounds.y + (bounds.height / 2);

        bool topQuadrant = (gameObject.transform.position.y > horizontalMidpoint);
        bool bottomQuadrant = (gameObject.transform.position.y < horizontalMidpoint);

        if (gameObject.transform.position.x < verticalMidpoint)
        {
            if (topQuadrant)
            {
                index = 1;
            }
            else if (bottomQuadrant)
            {
                index = 2;
            }
        }
        else if (gameObject.transform.position.x > verticalMidpoint)
        {
            if (topQuadrant)
            {
                index = 0;
            }
            else if (bottomQuadrant)
            {
                index = 3;
            }
        }

        return index;
    }

    public void Insert(GameObject gameObject)
    {
        if (nodes[0] != null)
        {
            int index = GetIndex(gameObject);

            if (index != -1)
            {
                nodes[index].Insert(gameObject);
                return;
            }
        }

        objects.Add(gameObject);

        if (objects.Count > maxObjects && level < maxLevels)
        {
            if (nodes[0] == null)
            {
                Split();
            }

            int i = 0;
            while (i < objects.Count)
            {
                int index = GetIndex(objects[i]);
                if (index != -1)
                {
                    nodes[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject gameObject)
    {
        int index = GetIndex(gameObject);
        if (nodes[0] != null && index != -1)
        {
            nodes[index].Retrieve(returnObjects, gameObject);
        }

        returnObjects.AddRange(objects);

        return returnObjects;
    }
    private void OnDrawGizmos()
    {
        DrawGizmos(this);
    }

    // This is a recursive function that will draw the Gizmos for the quadtree nodes
    private void DrawGizmos(Quadtree node)
    {
        if (node == null)
        {
            return;
        }

        // Draw the boundary of the current node
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(node.bounds.xMin, node.bounds.yMin, 0), new Vector3(node.bounds.xMin, node.bounds.yMax, 0));
        Gizmos.DrawLine(new Vector3(node.bounds.xMin, node.bounds.yMax, 0), new Vector3(node.bounds.xMax, node.bounds.yMax, 0));
        Gizmos.DrawLine(new Vector3(node.bounds.xMax, node.bounds.yMax, 0), new Vector3(node.bounds.xMax, node.bounds.yMin, 0));
        Gizmos.DrawLine(new Vector3(node.bounds.xMax, node.bounds.yMin, 0), new Vector3(node.bounds.xMin, node.bounds.yMin, 0));

        if (node.nodes != null)
        {
            for (int i = 0; i < node.nodes.Length; i++)
            {
                DrawGizmos(node.nodes[i]);
            }
        }
    }
}
