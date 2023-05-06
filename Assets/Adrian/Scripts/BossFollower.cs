using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollower : MonoBehaviour
{
    public int prefabHealth = 10; // The prefab's initial health
    private Boss bossScript; // Reference to the Boss script
    private BoxCollider2D movementArea; // Reference to the BoxCollider2D component defining the movement area
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

    // Start is called before the first frame update
    void Start()
    {
        GameObject bossObject = GameObject.FindWithTag("Boss"); // Make sure your boss GameObject has the "Boss" tag
        if (bossObject != null)
        {
            bossScript = bossObject.GetComponent<Boss>();
        }

        // Find the BoxCollider2D component in the scene
        movementArea = GameObject.FindGameObjectWithTag("Zone").GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int damage)
    {
        prefabHealth -= damage;
        if (prefabHealth <= 0)
        {
            if (bossScript != null)
            {
                bossScript.OnPrefabDefeated();
            }
            // Handle the prefab's defeat (e.g., play a death animation or sound, destroy the prefab object, etc.)
        }
    }


    void Update()
    {
        if (Manager.Instance.isDialogFinished)
        {
            Move();
            ChangeDirection();
            

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
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().destroyBot == true)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().maxSpawnedPrefabs--;
            Destroy(gameObject);
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
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().destroyBot == true)
    //    {
    //        GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().maxSpawnedPrefabs--;
    //        Destroy(this.gameObject);
    //    }
    //}
}
