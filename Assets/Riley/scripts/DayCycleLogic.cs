using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayCycleLogic : MonoBehaviour
{
    private Light2D lit;
    [SerializeField]
    public Light2D torch;
    public float tim = 0;
    public Color Night = new Color(0.03f, 0.03f, 0.13f);
    public Color Eve = new Color(0.9f, 0.48f, 0.02f);
    public Color Day = new Color(1, 1, 1);
    public bool N2D = true;
    public float TimeLength = 1;
    private float MidNTresh = -1;
    private float NoonTresh = 3;
    public float SpeedOfTime = 0.01f;
    public Upgrades UpgradesUpgrades;
    public int Rerolls = 0;
    public int RerollsLeft;
    public bool NoonUpgrades = false;
    //public bool isNight;
    public AudioSource UpgradeSound;
    
    // Start is called before the first frame update
    void Start()
    {
        lit = GetComponent<Light2D>();
        MidNTresh = 0 - TimeLength;
        NoonTresh = 2 + TimeLength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Manager.Instance.isDialogFinished)
        {
            //if (tim <= -0.25 || tim >= -0.01f) UpgradesUpgrades.IsLevelingUp = true;
            //else UpgradesUpgrades.IsLevelingUp = false;
            if (tim <= 1) lit.color = Color.Lerp(Night, Eve, tim);

            else if (tim > 1) lit.color = Color.Lerp(Eve, Day, tim - 1);
            torch.intensity = Mathf.Lerp(1, 0, tim);

            tim += N2D ? SpeedOfTime : -SpeedOfTime;

            if (tim <= MidNTresh)
            {

                N2D = true;
                RerollsLeft = Rerolls;
                UpgradeSound.Play();
                UpgradesUpgrades.IsLevelingUp = true;
            }
            if (tim >= NoonTresh)
            {

                N2D = false;
                if (NoonUpgrades == true)
                {
                    RerollsLeft = Rerolls;
                    UpgradeSound.Play();
                    UpgradesUpgrades.IsLevelingUp = true;
                }
            }
        }
        

        
    }

    public void useReroll()
    {
        RerollsLeft--;
    }


}
