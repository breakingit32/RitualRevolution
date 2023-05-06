using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewDialougeManager : MonoBehaviour
{
    public float Speed;
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    public GameObject player;
    public int xray = 0;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;




    public void OpenDialogue(Message[] messages, Actor[] actors, GameObject gameobjects)
    {

        //player.GetComponent<FindMousePositionTest>().enabled = false;

        player.GetComponent<PlayerMovement>().enabled = false;


        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameobjects.GetComponent<CircleCollider2D>().enabled = false;
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        DisplayMessage();
    }
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {

        player.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<Canvas>().enabled = true;
        //player.GetComponent<FindMousePositionTest>().enabled = false;

        
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        DisplayMessage();
    }


    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(messageToDisplay.message));

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }
    IEnumerator TypeSentence(string sentence)
    {
        messageText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            //yield return new WaitForSeconds(0.0000001f);
            messageText.text += letter;
            yield return null;
        }
    }
    public void NextMessage()
    {
        xray++;
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {

            player.GetComponent<PlayerMovement>().enabled = true;

            Manager.Instance.DialogFinished();


            //player.GetComponent<FindMousePositionTest>().enabled = true;
            gameObject.GetComponent<Canvas>().enabled = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame

}
