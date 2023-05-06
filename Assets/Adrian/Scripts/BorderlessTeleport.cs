using UnityEngine;

public class BorderlessTeleport : MonoBehaviour
{
    public float mapWidth = 10.0f;
    public float mapHeight = 10.0f;

    private Vector3 currentPosition;
    private void Update()
    {
        currentPosition = transform.position;

        if (currentPosition.x > mapWidth / 2)
        {
            currentPosition.x = -mapWidth / 2;
        }
        else if (currentPosition.x < -mapWidth / 2)
        {
            currentPosition.x = mapWidth / 2;
        }

        if (currentPosition.y > mapHeight / 2)
        {
            currentPosition.y = -mapHeight / 2;
        }
        else if (currentPosition.y < -mapHeight / 2)
        {
            currentPosition.y = mapHeight / 2;
        }

        transform.position = currentPosition;
    }
}
