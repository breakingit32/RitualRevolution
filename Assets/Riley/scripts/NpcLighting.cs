using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class NpcLighting : MonoBehaviour
{
    private GameObject LM;
    private Light2D Torch;
    // Start is called before the first frame update
    void Start()
    {
        LM = GameObject.FindGameObjectWithTag("LightingManager");
        Torch = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Torch.intensity = Mathf.Lerp(1, 0, LM.GetComponent<DayCycleLogic>().tim);
    }
}
