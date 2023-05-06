using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text FpsText;

    private float pollingTime = 0.75f;
    private float time;
    private int frameCOunt;

    private void Update()
    {
        time += Time.deltaTime;

        frameCOunt++;

        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCOunt / time);
            FpsText.text = frameRate.ToString();

            time -= pollingTime;
            frameCOunt = 0;
        }
    }
}
