using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    
    public bool IsLevelingUp;
    private RectTransform rct;
    // Start is called before the first frame update
    void Start()
    {
        rct = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (IsLevelingUp == true && rct.localScale.y < 1)
        {

            rct.localScale = new Vector3(1, rct.localScale.y + 0.1f, 1);
           
            
        }
        if (IsLevelingUp == true && rct.localScale.y >= 1) Time.timeScale = 0;
        else if (IsLevelingUp == false && rct.localScale.y > 0)
        {
            rct.localScale = new Vector3(1, rct.localScale.y - 0.1f, 1);
        }
        if (rct.localScale.y < 0) rct.localScale = new Vector3(1, 0, 1);
        if (rct.localScale.y > 1) rct.localScale = new Vector3(1, 1, 1);


    }

    //IEnumerator open()
    //{
    //    rct.localScale = new Vector3(1, rct.localScale.y + 0.1f, 1);
    //    yield return new WaitForSeconds(0.1f);
       
    //    Time.timeScale = 0f;
    //}
    //IEnumerator close()
    //{
    //    rct.localScale = new Vector3(1, rct.localScale.y - 0.1f, 1);
    //    yield return new WaitForSeconds(1f);
        
    //    Time.timeScale = 1f;
    //}

    public void FinishUpgrade()
    {
        //if (!dayCycleLogic.cardUsed)
        //{
        //    Time.timeScale = 1f;
        //    IsLevelingUp = false;
        //    dayCycleLogic.cardUsed = true;
        //}
        Time.timeScale = 1f;
        IsLevelingUp = false;
        
    }
    public void StartUpgrade()
    {
        IsLevelingUp = true;
    }


}
