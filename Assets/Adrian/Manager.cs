using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

   
    public bool isDialogFinished;

    private void Awake()
    {
        Application.targetFrameRate =60;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DialogFinished()
    {
        isDialogFinished = true;
    }
}
