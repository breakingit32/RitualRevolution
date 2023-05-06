using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject cardHand;
    public GameObject upgradeMenu;
    public GameObject pauseMenu;
    

    // Update is called once per frame
    void Update()
    {
        upgradeMenu.SetActive(!isPaused);
        cardHand.SetActive(!isPaused);
        pauseMenu.SetActive(isPaused);

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            //Debug.Log("WE PRESSED IT");
            switch (isPaused)
            {
                case true:
                    Resume();
                    break;

                case false:
                    Pause();
                    break;

            }
        }


    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Resume();
        SceneManager.LoadScene(0);
    }


}
