using UnityEngine;

public class BossFollowerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float changeDirectionTime = 2.0f;
    private float currentChangeDirectionTime;
    private Vector3 moveDirection;
    private BoxCollider2D movementArea; // Reference to the BoxCollider2D component defining the movement area

    void Start()
    {
        currentChangeDirectionTime = changeDirectionTime;
        ChooseNewDirection();

        // Find the BoxCollider2D component in the scene
        movementArea = GameObject.FindGameObjectWithTag("Zone").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
        ChangeDirection();
    }

    void Move()
    {
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Reflect the movement direction if the NPC is about to move outside the BoxCollider2D bounds
        if (newPosition.x < movementArea.bounds.min.x || newPosition.x > movementArea.bounds.max.x)
        {
            moveDirection.x = -moveDirection.x;
            UpdateRotation();
        }
        if (newPosition.y < movementArea.bounds.min.y || newPosition.y > movementArea.bounds.max.y)
        {
            moveDirection.y = -moveDirection.y;
            UpdateRotation();
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void ChangeDirection()
    {
        currentChangeDirectionTime -= Time.deltaTime;

        if (currentChangeDirectionTime <= 0)
        {
            ChooseNewDirection();
            currentChangeDirectionTime = changeDirectionTime;
        }
    }

    void ChooseNewDirection()
    {
        float angle = Random.Range(0, 360);
        moveDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;
        UpdateRotation();
    }

    void UpdateRotation()
    {
        float zRotation = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}
