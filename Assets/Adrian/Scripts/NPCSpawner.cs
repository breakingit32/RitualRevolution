using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform player;
    public float spawnRadius = 20.0f;
    public float minSpawnDistance = 10.0f;
    public int maxNPCs = 20;
    public float spawnInterval = 5.0f;

    private Camera mainCamera;
    private int currentNPCCount = 0;
    private Vector3 previousPlayerPosition;

    void Start()
    {
        mainCamera = Camera.main;
        previousPlayerPosition = player.position;
        StartSpawningNPCs();
    }

    async void StartSpawningNPCs()
    {
        while (currentNPCCount < maxNPCs)
        {
            await Task.Run(() => SpawnNPC());
            await Task.Delay((int)(spawnInterval * 1000));
        }
    }

    void SpawnNPC()
    {
        Vector3 playerDirection = (player.position - previousPlayerPosition).normalized;
        Vector3 spawnPosition = player.position + playerDirection * spawnRadius;
        spawnPosition = Vector3.Lerp(spawnPosition, player.position, Random.Range(0.5f, 1f));
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(spawnPosition);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            GameObject npc = ObjectPooler.instance.GetPooledObject();

            if(npc != null)
            {
                npc.transform.position = spawnPosition;
                npc.transform.rotation = Quaternion.identity;
                npc.SetActive(true);
            }
            //Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            currentNPCCount++;
        }

        previousPlayerPosition = player.position;
    }
}
