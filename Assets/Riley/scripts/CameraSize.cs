using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private Camera cam;
    public float ActualSize;
    public float SizeGoal;
    public float GrowthSpeed = 0.01f;
    public float ShrinkSpeed = 0.001f;
    public float ZoomAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Makes sure actual size cant go below zero and that it cant go higher than size goal
        if (ActualSize < 0) ActualSize = 0;
        if (ActualSize > SizeGoal) ActualSize = SizeGoal;

        cam.orthographicSize = 10 + ActualSize;

        if (ActualSize < SizeGoal) ActualSize += GrowthSpeed; //If the actual size is smaller than the goal, Grow
        if (SizeGoal > 0) SizeGoal -= ShrinkSpeed; //if goal is greater than zero, shrink the goal
        if (SizeGoal < 0) SizeGoal = 0;

        if (Manager.Instance.isDialogFinished)
        {
            int npcLayerMask = 1 << LayerMask.NameToLayer("NPC");
            int cLayerMask = 1 << LayerMask.NameToLayer("C");
            gameObject.GetComponent<Camera>().cullingMask |= npcLayerMask;
            gameObject.GetComponent<Camera>().cullingMask &= ~cLayerMask;
        }
    }

    public void ZoomOut()
    {
        SizeGoal += ZoomAmount;
    }



}
