using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float baseOrthographicSize = 5.0f;
    public float zoomFactor = 0.5f;
    public LayerMask npcLayerMask; // Add this line
    public float npcCountingRadius = 20.0f; // Add this line

    private Camera cam;
    private int followerCount;

    //Cashed Var
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    private Collider2D[] npcs;
    private int currentFollowerCount;
    private Transform gameObjectTransform;
    void awake()
    {
        cam = GetComponent<Camera>();

    }

    void Update()
    {
        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        npcs = Physics2D.OverlapCircleAll(target.position, npcCountingRadius, npcLayerMask); // Add this line
        currentFollowerCount = npcs.Length; // Update this line

        if (currentFollowerCount != followerCount)
        {
            cam.orthographicSize = baseOrthographicSize + Mathf.Sqrt(currentFollowerCount) * zoomFactor;
            followerCount = currentFollowerCount;
        }
    }
}