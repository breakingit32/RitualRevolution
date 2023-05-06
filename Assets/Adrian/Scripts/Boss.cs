using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to be spawned
    public float spawnInterval = 2f; // The interval between spawns in seconds
    public int maxSpawnedPrefabs = 5; // Maximum number of spawned prefabs
    public int bossHealth = 100; // Boss's initial health
    public BoxCollider2D bossArea; // Reference to the BoxCollider2D defining the spawn area
    public LayerMask floorLayerMask; // LayerMask for the floor tilemap layer
    public Tilemap spawnTilemap; // Reference to the Tilemap component
    public LayerMask obstacleLayer; // LayerMask for the obstacles
    public float checkRadius = 0.2f; // Radius for checking if there's an obstacle at the spawn position
    public GameObject text;
    public int currentSpawnedPrefabs = 0; // Counter for the number of spawned prefabs
    private float spawnTimer = 0f; // Timer for spawning prefabs
    public float fadeInDuration = 2.0f;
    public Image image;
    public Color imageColor;
    public GameObject imagess;
    public float fadeOutDuration = 2.0f;
    public GameObject holder;
    public GameObject daylikecycle;
    public GameObject restart;
    public GameObject stop;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && currentSpawnedPrefabs < maxSpawnedPrefabs)
        {
            SpawnPrefab();
            spawnTimer = 0f;
        }
        
        if(maxSpawnedPrefabs == 0)
        {
            daylikecycle.SetActive(false);
            holder.SetActive(false);
            imagess.SetActive(true);
            StartCoroutine(FadeInEffect());
            text.SetActive(true);
            restart.SetActive(true);
            stop.SetActive(true);   
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

        }
    }
    IEnumerator FadeInEffect()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = elapsedTime / fadeOutDuration;
            image.color = imageColor;
            yield return null;
        }

        imageColor.a = 1;
        image.color = imageColor;
      
    }


    public void OnPrefabDefeated()
    {
        currentSpawnedPrefabs--;
        bossHealth--;

        if (bossHealth <= 0)
        {
            // Handle the boss's defeat (e.g., play a death animation or sound, destroy the boss object, etc.)
        }
    }
    void SpawnPrefab()
    {
        Debug.Log("Spawning!!!");
        Vector3 spawnPosition = FindValidSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            currentSpawnedPrefabs++;
        }
    }
    Vector3 FindValidSpawnPosition()
    {
        Vector3 spawnPosition;
        bool validPosition = false;

        do
        {
            float xPos = Random.Range(bossArea.bounds.min.x, bossArea.bounds.max.x);
            float yPos = Random.Range(bossArea.bounds.min.y, bossArea.bounds.max.y);
            spawnPosition = new Vector3(xPos, yPos, 0);

            Vector3Int tilemapPosition = spawnTilemap.WorldToCell(spawnPosition);

            if (!Physics2D.OverlapCircle(spawnPosition, checkRadius, obstacleLayer) && spawnTilemap.HasTile(tilemapPosition))
            {
                validPosition = true;
            }
        } while (!validPosition);

        return spawnPosition;
    }


}
