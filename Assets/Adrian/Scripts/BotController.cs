using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 180.0f;
    public float changeDirectionTime = 2.0f;
    public float activeRadius = 20.0f;
    public LayerMask npcLayer;
    public Rect worldSize;
    public int maxLevels;
    public LayerMask wallLayer; // LayerMask for the wall layer

    private float currentChangeDirectionTime;
    private Vector3 moveDirection;
    private Quadtree quadtree;
    private Quaternion targetRotation;

    private float angle;
    private float zRotation;
    private Collider2D[] npcs;
    private List<GameObject> returnObjects;
    private Collider2D npcCollider;
    private Transform myTransform;
    void Start()
    {
        currentChangeDirectionTime = changeDirectionTime;
        ChooseNewDirection();
        quadtree = new Quadtree(0, worldSize, maxLevels);
        myTransform= transform;
    }

    void Update()
    {
        if (Manager.Instance.isDialogFinished)
        {
            Move();
            ChangeDirection();
            UpdateNearbyNPCs();

            // Rotate the bot towards the moveDirection
            targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            myTransform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

    }


    void Move()
    {
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
        angle = Random.Range(0, 360);
        moveDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;

        // Set the targetRotation based on the moveDirection
        zRotation = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, 0, zRotation);
    }


    void UpdateNearbyNPCs()
    {
        quadtree.Clear();
        npcs = Physics2D.OverlapCircleAll(transform.position, activeRadius, npcLayer);

        foreach (Collider2D npcCollider in npcs)
        {
            quadtree.Insert(npcCollider.gameObject);
        }

        returnObjects = new List<GameObject>();
        foreach (Collider2D npcCollider in npcs)
        {
            returnObjects.Clear();
            //quadtree.Retrieve(returnObjects, npcCollider.gameObject);

            //NPCFollower npcFollower = npcCollider.GetComponent<NPCFollower>();
            //if (npcFollower != null && !npcFollower.isFollowing)
            //{
            //    npcFollower.FindNewTarget(returnObjects);
            //}
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ReflectDirection(collision.contacts[0].normal);
        }
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            ReflectDirection(collision.contacts[0].normal);
        }
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().destroyBot == true)
        {
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<NPCGridSpawner>().numBots--;
            Destroy(gameObject);
        }
    }
    void ReflectDirection(Vector2 normal)
    {
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
        zRotation = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }


}
