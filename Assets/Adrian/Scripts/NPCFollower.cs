using UnityEngine;

public class NPCFollower : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 2.0f;
    public float minDistance = 1.0f;
    public float maxDistance = 3.0f;
    public LayerMask followerLayer;
    public float noiseFactor = 0.5f;

    private bool following = false;
    private Vector3 targetPosition;
    private Vector3 offset;
    private Transform leader;
    public static int FollowerCount { get; private set; } = 0;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        offset = new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance), 0);
        targetPosition = player.transform.position + offset;
    }

    void FixedUpdate()
    {
        if (following)
        {
            targetPosition = leader.position + offset;
            MoveTowardsTargetPosition();

            Vector3 avoidance = AvoidOtherFollowers();
            transform.position += avoidance * Time.deltaTime;

            offset += new Vector3(Random.Range(-noiseFactor, noiseFactor), Random.Range(-noiseFactor, noiseFactor), 0);
            offset = Vector3.ClampMagnitude(offset, maxDistance);
        }
    }

    void MoveTowardsTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!following && (other.gameObject == player || other.gameObject.CompareTag("Follower") || other.gameObject.CompareTag("Bot")))
        {
            StartFollowing(other.transform);
        }
    }



    void StartFollowing(Transform triggerObject)
    {
        if (!following)
        {
            following = true;
            //gameObject.tag = "Follower";
            leader = triggerObject;
            GetComponent<SpriteRenderer>().color = triggerObject.GetComponent<SpriteRenderer>().color;
            FollowerCount++;
        }
    }

    Vector3 AvoidOtherFollowers()
    {
        Vector3 avoidance = Vector3.zero;
        Collider2D[] nearbyFollowers = Physics2D.OverlapCircleAll(transform.position, minDistance, followerLayer);

        foreach (Collider2D followerCollider in nearbyFollowers)
        {
            if (followerCollider.gameObject != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, followerCollider.transform.position);
                if (distance < minDistance)
                {
                    avoidance += (transform.position - followerCollider.transform.position).normalized * (minDistance - distance);
                }
            }
        }

        return avoidance;
    }
}

