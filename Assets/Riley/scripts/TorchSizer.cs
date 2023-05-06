using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class TorchSizer : MonoBehaviour
{
    private Light2D lit;
    public float torchRadius = 0;
    // Start is called before the first frame update
    void Start()
    {
        lit = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lit.pointLightInnerRadius = torchRadius / 2;
        lit.pointLightOuterRadius = torchRadius;
    }
}
