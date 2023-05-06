using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
    public bool GateClosed = true;
    private BoxCollider2D bc2;
    private Animator ani;
    
    // Start is called before the first frame update
    void Start()
    {
        bc2 = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ani.SetBool("IsClosed", GateClosed);
        bc2.enabled = GateClosed;


    }
}
