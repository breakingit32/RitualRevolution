using UnityEngine;

public class Bot : MonoBehaviour
{
    public BotFlowField botFlowField;
    public float updateFrequency = 1f;
    private float lastUpdateTime = 0f;

    private void Update()
    {
        if (Time.time - lastUpdateTime >= updateFrequency)
        {
            botFlowField.GenerateFlowField(transform.position);
            lastUpdateTime = Time.time;
        }
    }
}
