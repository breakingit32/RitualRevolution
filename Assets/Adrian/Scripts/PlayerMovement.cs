using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public FlowField flowField;
    public float speed = 5.0f;
    private Vector2 targetPosition;
    private bool isMoving;
    public int damage = 1;
    public int followPlayerCount;
    public Quadtree quadtree { get; private set; }
    private Rigidbody2D rb;
    public BotFlowField botFlowField;
    public float updateFrequency = 1f;
    private float lastUpdateTime = 0f;
    public float boxCastWidth = 5f;
    public float boxCastHeight = 5f;
    public LayerMask npcLayer;
    public float rotationOffset = 0f;
    public GameObject spriteToEnable;
    public int riquredNPCNum;
    public GameObject[] npcs;
    public bool destorys;
    private Vector2 previousPosition;
    public float currentSpeed;
    public Animator AnimatoranimatorAnimator;
    public bool destroyBot;
    public bool canConvert;
    private float updateSpeedInterval = 0.2f;
    private float distance;
    private Vector2 direction;
    private Vector2 newPosition;
    private Plane plane;
    private Ray ray;

 
    void Start()
    {
        targetPosition = transform.position;
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
        AnimatoranimatorAnimator = gameObject.GetComponent<Animator>();
        StartCoroutine(UpdateSpeedRoutine());
    }

    void Update()
    {
        if(Manager.Instance.isDialogFinished)
        {
            if (destorys)
            {
                foreach (GameObject npc in npcs)
                {
                    npc.GetComponent<NPCController>().destroy = true;
                }
            }

            MoveToTarget();
            quadtree = new Quadtree(10, 5, new Rect(-50, -50, 100, 100));

            if (Input.GetMouseButton(0))
            {
                plane = new Plane(Vector3.forward, Vector3.zero);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 worldMousePosition = ray.GetPoint(distance);
                    targetPosition = new Vector2(worldMousePosition.x, worldMousePosition.y);
                    RotateToFaceCursor(worldMousePosition);
                    isMoving = true;
                }
            }
            else if (!Input.GetMouseButton(0))
            {
                gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else
            {
                isMoving = false;
            }
            if (Time.time - lastUpdateTime >= updateFrequency)
            {
                flowField.GenerateFlowField(transform.position);
                lastUpdateTime = Time.time;
            }
            CheckForFollowingNPCs();

        }

        //if(followPlayerCount % riquredNPCNum == 0) openCardPick();

    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    BossFollower prefab = collision.GetComponent<BossFollower>();
    //    if (prefab != null)
    //    {
    //        prefab.TakeDamage(damage);
    //        Destroy(gameObject); // Destroy the projectile
    //    }
    //}

    void FixedUpdate()
    {
        npcs = GameObject.FindGameObjectsWithTag("FollowPlayer");
        MoveToTarget();
    }
    void RotateToFaceCursor(Vector3 cursorWorldPosition)
    {
        Vector3 direction = cursorWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Add the rotation offset to the calculated angle
        angle += rotationOffset;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void CheckForFollowingNPCs()
    {
        Vector2 boxCastSize = new Vector2(boxCastWidth, boxCastHeight);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, Vector2.zero, 0f, npcLayer);

        followPlayerCount = 0;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("FollowPlayer"))
            {
                followPlayerCount++;
            }
        }
        followPlayerCount = followPlayerCount / 2;

        //Debug.Log("Number of NPCs with FollowPlayer tag: " + followPlayerCount + " mod " + followPlayerCount %15);
    }
    void MoveToTarget()
    {
        if (isMoving)
        {
            distance = Vector2.Distance(rb.position, targetPosition);

            if (distance > 0.1f)
            {
                direction = (targetPosition - rb.position).normalized;
                newPosition = rb.position + direction * speed * Time.deltaTime;
                rb.MovePosition(newPosition);
            }
        }
    }
    IEnumerator UpdateSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateSpeedInterval);

            currentSpeed = ((Vector2)transform.position - previousPosition).magnitude / Time.fixedDeltaTime;
            previousPosition = transform.position;
            AnimatoranimatorAnimator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        }
    }
}
