using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    public float formationRadius = 5f;
    public float formationSpeed = 2f;
    public Transform leader;
    private List<Transform> followers = new List<Transform>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            followers.Add(child);
        }
    }

    void Update()
    {
        if (leader != null)
        {
            //Vector2 formationCenter = leader.position;
            //float angleIncrement = 360f / followers.Count;
            //float currentAngle = 0f;

            //foreach (Transform follower in followers)
            //{
            //    //Vector2 targetPosition = formationCenter + Quaternion.Euler(0f, 0f, currentAngle) * (Vector2.right * formationRadius);

            //    follower.position = Vector2.MoveTowards(follower.position, targetPosition, formationSpeed * Time.deltaTime);
            //    currentAngle += angleIncrement;
            //}
        }

    }

    public void AddFollower(Transform follower)
    {
        followers.Add(follower);
    }

    public void RemoveFollower(Transform follower)
    {
        followers.Remove(follower);
    }
}
