using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public FlowField flowField;
    public BotFlowField botFlowField;
    public float speed;
    public bool isFollowing = false;
    public LayerMask npcLayerMask;
    public bool isFollowingBot = false;
    public float rotationSpeed = 50f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform leaderTransform;
    public bool destroy;
    private Vector2 previousPosition;
    public float currentSpeed;
    public Animator AnimatoranimatorAnimator;
    private Rigidbody2D leaderRigidbody;
    private GameObject player;
    private float updateSpeedInterval = 0.2f;
    public PlayerMovement playerMovement;
    private CircleCollider2D npcColider;
    public AudioClip clip;
    public bool soundPlayed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npcColider = GetComponent<CircleCollider2D>();
        // Get the PlayerMovement component
        playerMovement = player.GetComponent<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        AnimatoranimatorAnimator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject flowFieldObject = GameObject.Find("FlowField");
        if (flowFieldObject != null)
        {
            flowField = flowFieldObject.GetComponent<FlowField>();
        }
        else
        {
            Debug.LogError("FlowField GameObject not found!");
        }
        StartCoroutine(UpdateSpeedRoutine());
    }

    void FixedUpdate()
    {
        speed = playerMovement.speed;
        if (isFollowing)
        {
            Vector2 direction;

            if (!isFollowingBot && flowField != null)
            {
                direction = flowField.GetDirectionFromPosition(transform.position, npcLayerMask);
            }
            else if (isFollowingBot && botFlowField != null)
            {
                direction = botFlowField.GetDirectionFromPosition(transform.position, npcLayerMask);
            }
            else
            {
                direction = Vector2.zero;
            }

            rb.velocity = direction * speed;

            // Set the NPC's rotation to the leader's rotation
            if (leaderTransform != null)
            {
                transform.rotation = leaderTransform.rotation;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log(other.tag);
    //    if (other.CompareTag("Player") )
    //    {
    //        isFollowing = true;
    //        isFollowingBot = false;
    //        botFlowField = null;
    //        flowField = other.GetComponent<PlayerMovement>().flowField;
    //        // Change the NPC's color to match the bot's color
    //        SpriteRenderer playerSpriteRenderer = other.GetComponent<SpriteRenderer>();
    //        if (playerSpriteRenderer != null)
    //        {
    //            spriteRenderer.color = playerSpriteRenderer.color;
    //        }
    //        leaderTransform = other.transform;
    //        this.gameObject.tag = "FollowPlayer";
    //        //Debug.Log("NPC is now following Player");
    //    }
    //    else if (other.CompareTag("Bot")) // Replace "Bot" with the appropriate tag for your bot
    //    {
    //        isFollowing = true;
    //        isFollowingBot = true;
    //        flowField = null;
    //        botFlowField = other.GetComponent<Bot>().botFlowField;

    //        // Change the NPC's color to match the bot's color
    //        SpriteRenderer botSpriteRenderer = other.GetComponent<SpriteRenderer>();
    //        if (botSpriteRenderer != null)
    //        {
    //            spriteRenderer.color = botSpriteRenderer.color;
    //        }
    //        leaderRigidbody = other.GetComponent<Rigidbody2D>();
    //        gameObject.GetComponent<Rigidbody2D>().velocity= leaderRigidbody.velocity;
    //        gameObject.GetComponent<Rigidbody2D>().angularVelocity = leaderRigidbody.angularVelocity;
    //        //leaderTransform = other.transform;
    //        this.gameObject.tag = "FollowBot";
    //        //Debug.Log("NPC is now following Bot");

    //    }
    //    else if (other.CompareTag("Messup") && this.gameObject.CompareTag("FollowBot"))
    //    {
    //        StartCoroutine(interuptFollow(15f));
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Messup") && this.gameObject.CompareTag("FollowBot"))
        {
            Debug.Log("Messup");
            StartCoroutine(interuptFollow(15f));
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the other object has the "NeutralNPC" tag
        if ((other.gameObject.CompareTag("NeuteralNPC") || other.gameObject.CompareTag("FollowBot")) &&
            playerMovement.canConvert == true && gameObject.CompareTag("FollowPlayer"))
        {
            // Get the NPCController of the other NPC
            NPCController otherNPCController = other.gameObject.GetComponent<NPCController>();

            if (this.gameObject.CompareTag("FollowPlayer"))
            {
                otherNPCController.isFollowing = true;
                otherNPCController.isFollowingBot = false;
                otherNPCController.botFlowField = null;
                otherNPCController.flowField = this.flowField;

                SpriteRenderer otherSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
                if (otherSpriteRenderer != null)
                {
                    otherSpriteRenderer.color = spriteRenderer.color;
                }

                otherNPCController.leaderTransform = this.leaderTransform;
                otherNPCController.gameObject.tag = "FollowPlayer";
            }
            else if (this.gameObject.CompareTag("FollowBot"))
            {
                otherNPCController.isFollowing = true;
                otherNPCController.isFollowingBot = true;
                otherNPCController.flowField = null;
                otherNPCController.botFlowField = this.botFlowField;

                SpriteRenderer otherSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
                if (otherSpriteRenderer != null)
                {
                    otherSpriteRenderer.color = spriteRenderer.color;
                }

                otherNPCController.leaderTransform = this.leaderTransform;
                otherNPCController.gameObject.tag = "FollowBot";
            }
        }
        // Existing code for the "FollowBot" tag and destroy flag
        else if (other.gameObject.CompareTag("FollowBot") && destroy == true && this.gameObject.tag != "FollowBot" && this.gameObject.tag != "NeuteralNPC")
        {
            Destroy(other.gameObject);
            //Debug.Log("Destoy");
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            isFollowing = true;
            isFollowingBot = false;
            botFlowField = null;
            flowField = other.gameObject.GetComponent<PlayerMovement>().flowField;
            
            SpriteRenderer playerSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null)
            {
                
                spriteRenderer.color = playerSpriteRenderer.color;
            }
            if (!soundPlayed) {
                gameObject.GetComponent<AudioSource>().PlayOneShot(clip, 0.7f);
                soundPlayed = true;}
            
            leaderTransform = other.transform;
            this.gameObject.tag = "FollowPlayer";
        }
        else if (other.gameObject.CompareTag("Bot"))
        {
            isFollowing = true;
            isFollowingBot = true;
            flowField = null;
            botFlowField = other.gameObject.GetComponent<Bot>().botFlowField;

            SpriteRenderer botSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            if (botSpriteRenderer != null)
            {
                spriteRenderer.color = botSpriteRenderer.color;
            }

            leaderRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().velocity = leaderRigidbody.velocity;
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = leaderRigidbody.angularVelocity;
            this.gameObject.tag = "FollowBot";
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
    IEnumerator interuptFollow(float time)
    {
        npcColider.enabled = false;
        isFollowingBot = false;
        yield return new WaitForSeconds(time);
        isFollowingBot= true;
        npcColider.enabled = true;
    }
}
