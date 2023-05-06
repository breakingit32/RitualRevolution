using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneButton : MonoBehaviour
{
    public int SceneNumber;
    

    public void GoToScene()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
