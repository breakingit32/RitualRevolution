using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RtChildLogic : MonoBehaviour
{
    public RitualLogic rtl;
    private Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        txt.text = rtl.CardNumber.ToString();
        
        gameObject.SetActive(rtl.CardNumber > 0);
    }
}
