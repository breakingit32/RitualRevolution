using UnityEngine;

public class NPCRepulsion : MonoBehaviour
{
    public float repulsionForce = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.CompareTag("NPC"))
        //{
        //    Vector2 direction = (transform.position - other.transform.position).normalized;
        //    rb.AddForce(direction * repulsionForce, ForceMode2D.Force);
        //}
    }
}
