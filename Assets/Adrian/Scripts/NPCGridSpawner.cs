using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
public class NPCGridSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int gridSizeX = 100;
    public int gridSizeY = 100;
    public int npcCount = 1000;
    public float spacing = 1.0f;
    public float minDensity = 0.1f;
    public float maxDensity = 1.0f;
    public GameObject botPrefab;
    public int numBots;
    public LayerMask obstacleLayer;
    public float checkRadius = 0.2f;
    public Tilemap spawnTilemap;
    public Animator AnimatoranimatorAnimator;
    public BoxCollider2D box;
    public GameObject wall;
    public Camera mainCamera;
    public Vector3 cameraTargetPosition;
    public float cameraMoveDuration = 3.0f;
    private bool cameraMoved = false;
    public float ninty;
    public GateLogic gate;
    void Update()
    {
        if (numBots <=ninty && !cameraMoved)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
            StartCoroutine(MoveCameraToTargetAndBack());
            cameraMoved = true;
           
        }
    }

    IEnumerator MoveCameraToTargetAndBack()
    {
        float elapsedTime = 0;
        Vector3 originalCameraPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y, originalCameraPosition.z);

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, targetPosition, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = targetPosition;

        //AnimatoranimatorAnimator.SetBool("IsClosed", false);
        box.enabled = false;
        wall.layer = 0;
        gate.GateClosed = false;
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().enabled = true;

        float pauseDuration = 3.0f;
        yield return new WaitForSeconds(pauseDuration);

        elapsedTime = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        playerPosition.z = originalCameraPosition.z;

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(targetPosition, playerPosition, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = playerPosition;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = true;
    }

    void Start()
    {
        ninty = numBots * 0.2f;
        SpawnNPCs();
        numBots++;
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            npc.GetComponent<SpriteRenderer>().color = Color.black;
        }

        for (int i = 0; i < numBots; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            GameObject botInstance = Instantiate(botPrefab, spawnPosition, Quaternion.identity);
            botInstance.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition;
        bool validPosition = false;

        do
        {
            float xPos = Random.Range(-gridSizeX * spacing / 2, gridSizeX * spacing / 2);
            float yPos = Random.Range(-gridSizeY * spacing / 2, gridSizeY * spacing / 2);
            spawnPosition = new Vector3(xPos, yPos, 0);

            Vector3Int tilemapPosition = spawnTilemap.WorldToCell(spawnPosition);

            if (!Physics2D.OverlapCircle(spawnPosition, checkRadius, obstacleLayer) && spawnTilemap.HasTile(tilemapPosition) && !IsWithinWallCollider(spawnPosition))
            {
                validPosition = true;
            }
        } while (!validPosition);

        return spawnPosition;
    }

    bool IsWithinWallCollider(Vector3 position)
    {
        Collider2D wallCollider = Physics2D.OverlapCircle(position, checkRadius, obstacleLayer);
        if (wallCollider != null && wallCollider.gameObject.tag == "Wall")
        {
            return true;
        }
        return false;
    }

  
        
}
