using UnityEngine;
using UnityEngine.UI;

public class CardOptions : MonoBehaviour
{
    public GameObject cardOptionsPanel;
    public int NPCcount;

    private void Start()
    {
        NPCcount = GetComponent<PlayerMovement>().followPlayerCount;
        Debug.Log(cardOptionsPanel.name);
        cardOptionsPanel.SetActive(false);
    }

    private void Update()
    {
        if(NPCcount >= 10)ShowCardOptions();
    }

    public void ShowCardOptions()
    {
        Time.timeScale = 0; // Pause the game
        cardOptionsPanel.SetActive(true);
    }

    public void HideCardOptions()
    {
        Time.timeScale = 1; // Resume the game
        cardOptionsPanel.SetActive(false);
    }

    public void SelectCard(int cardId)
    {
        // Apply the card effect based on the cardId
        // e.g., playerCards.ApplyFleetFooted();

        HideCardOptions();
    }
}

