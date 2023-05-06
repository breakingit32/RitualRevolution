using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollLogic : MonoBehaviour
{
    public DayCycleLogic DCL;
    private Button btn;


    private void Start()
    {
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        btn.interactable= (DCL.RerollsLeft > 0);
        
    }
}
