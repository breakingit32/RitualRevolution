using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RitualLogic : MonoBehaviour
{
    public int CardNumber = 0;
    public float BottomY = -650;
    public float TopY = -340;
    public float moveSpeed = 10;
    private bool isHighlighted = false;
    private RectTransform rt;
    private Button btn;
    public Text txt;
    private GameObject Player;
    public GameObject CircleCollider2D;
    private float lastPlayerRotationZ;
    private float lastPlayerAngularVelocity;
    private Vector3 lastPlayerPosition;
    public FlowField flowField;
    private TooltipController ToolTip;
    public float spawnRadius = 5f;
    public int maxSpawnAttempts = 10;
    public GameObject ToolTipObj;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        btn = GetComponent<Button>();
        Player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerRotationZ = Player.transform.eulerAngles.z;
        lastPlayerPosition = Player.transform.position;
        
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHighlighted == true && rt.anchoredPosition.y < TopY && btn.interactable)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + moveSpeed);
            ToolTipObj.SetActive(true);
        }

        if (isHighlighted == false && rt.anchoredPosition.y > BottomY && btn.interactable)
        { 
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y - moveSpeed);
            
        }
        else if(!btn.interactable) 
        {
            ToolTipObj.SetActive(false);
        }
        btn.interactable = (CardNumber > 0);
        txt.text = CardNumber.ToString();
        txt.enabled = (CardNumber > 0);
        
    }




    public void IncreaseNPCs()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("FollowPlayer");
        int currentNPCs = npcs.Length;
        int newNPCs = Mathf.CeilToInt(currentNPCs * 0.25f);

        // Access the PlayerMovement component from the player
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        for (int i = 0; i < newNPCs; i++)
        {
            GameObject npc = ObjectPooler.instance.GetPooledObject();
            if (npc != null)
            {
                Vector3 spawnPosition = Player.transform.position;
                npc.transform.position = spawnPosition;
                npc.transform.rotation = Quaternion.identity;
                npc.SetActive(true);
                npc.tag = "FollowPlayer";
                npc.GetComponent<NPCController>().flowField = GameObject.Find("Player").GetComponent<FlowField>();
            }
        }
    }





    public void speedUp()
    {
        StartCoroutine(inscreaseSpeedForTime(10, 1.25f));
    }

    public void deInfluence()
    {
        StartCoroutine(fireRing());
    }

    IEnumerator RotateCircleCollider2D(float time, float zRotationSpeed)
    {
        float startTime = Time.time;
        Vector3 lastPlayerPosition = Player.transform.position;
        float initialChildRotationZ = CircleCollider2D.transform.localEulerAngles.z;

        while (Time.time - startTime < time)
        {
            Vector3 playerPosition = Player.transform.position;

            // Set the circle's position to match the player's position
            CircleCollider2D.transform.position = playerPosition;

            // Rotate the circle based on the zRotationSpeed
            initialChildRotationZ += zRotationSpeed * Time.deltaTime;
            CircleCollider2D.transform.localRotation = Quaternion.Euler(0, 0, initialChildRotationZ);

            yield return null;
        }
    }




    public void isEntered()
    {
        isHighlighted = true;
    }

    public void isExited()
    {
        isHighlighted = false;
    }

    public void decreaseCardCount()
    {
        CardNumber--;
    }

    public void increaseCardCount()
    {
        CardNumber++;
    }

    IEnumerator inscreaseSpeedForTime(int time, float speed)
    {
        Player.GetComponent<PlayerMovement>().speed *= speed;
        yield return new WaitForSeconds(time);
        Player.GetComponent<PlayerMovement>().speed /= speed;
    }
    IEnumerator fireRing()
    {
        CircleCollider2D.SetActive(true);
        StartCoroutine(RotateCircleCollider2D(15f, 100f));
        yield return new WaitForSeconds(15f);
        CircleCollider2D.SetActive(false);
    }
}

