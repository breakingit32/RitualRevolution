using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    
    public int cardNumber = 0;
    public Upgrades upg;
    public Sprite card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12, card13, card14, card15;
    public RitualLogic rc1, rc2, rc3, rc4, rc5;
    private Image img;
    public TorchSizer ts;
    public DayCycleLogic DCL;
    public GameObject player;
    public GameObject fourthOption;
    public GameObject Minimap;
    public Camera mapCam;
    private TooltipController ToolTip;
    void Start()
    {
        img = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        mapCam = mapCam.GetComponent<Camera>();
        ToolTip = GetComponentInChildren<TooltipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upg.IsLevelingUp == false) cardNumber = 0;
        else if (cardNumber == 0) cardNumber = Random.Range(1, 16);

        switch (cardNumber)
        {
            default:
                break;

            case 1://stackable //Torch card

                img.sprite = card1;
                ToolTip.SetDescription("Torch holding habit:\nIts probably safe to carry this many torches. Increase light radius at night");
                break;

            case 2: //MiniMap
                img.sprite = card2;
                ToolTip.SetDescription("Cartography Classes:\nWhile annoying their constant map making IS helpful. Enable Minimap (This is the best way to track rival leaders)");
                if(Minimap.activeSelf==true) cardNumber = Random.Range(1, 16);
                break;

            case 3: //Destory npcs only
                img.sprite = card3;
                ToolTip.SetDescription("Merderous Habit:\nAbsolutely not a good idea. Your followers can now destroy other followers (not the leader though)");
                if(player.GetComponent<PlayerMovement>().destorys == true) cardNumber = Random.Range(1, 16);
                break;

            case 4://permenent speed upgrade
                img.sprite = card4;
                ToolTip.SetDescription("Cardio regime:\nNever skip leg day. Increase Speed 10%");
                //Stackable
                break;

            case 5: //Destory Bots
                img.sprite = card5;
                ToolTip.SetDescription("Carry Guilotine:\nWhat do you mean there are other forms of execution!? Allows you to destory Other Leaders (Required to win the game)");
                if (player.GetComponent<PlayerMovement>().destroyBot == true) cardNumber = Random.Range(1, 16);
                //player.GetComponent<PlayerMovement>().destroyBot
                break;

            case 6: //Can Convert
                img.sprite = card6;
                ToolTip.SetDescription("Proselytization habit:\nThe new robes make them must more convincing! Your followers gain the ability to convert other followers");
                if (player.GetComponent<PlayerMovement>().canConvert == true) cardNumber = Random.Range(1, 16);
                //player.GetComponent <PlayerMovement>().canConvert
                break;

            case 7: //Forth option
                img.sprite = card7;
                ToolTip.SetDescription("Card Training:\nYou can now hold four cards in your hand at a time! Add Extra Choice Card");
                if (fourthOption.activeSelf == true) cardNumber = Random.Range(1, 16);
                break;

            case 8: //Enable dual choice
                ToolTip.SetDescription("Chronal Epithany:\nSo what if you can see the sun? Its still 12 on the clock! Enable Card Choice at Noon");
                img.sprite = card8;
                if (DCL.NoonUpgrades == true) cardNumber = Random.Range(1, 16);
                //DCL.NoonUpgrades = true;
                break;

            case 9: //Increase day speed
                img.sprite = card9;
                ToolTip.SetDescription("Fun Habit: \nTime flies when youre having fun. Increase Day Speed");
                if(DCL.SpeedOfTime >= 0.04f) cardNumber = Random.Range(1, 16);
                //stackable to a limit
                break;

            case 10:
                // Reroll
                ToolTip.SetDescription("Divining Habit:\nThese fortune telling classes have been paying off. Enables a reroll option for the future");
                img.sprite = card10;
                if(DCL.Rerolls>=3) cardNumber = Random.Range(1, 16);
                break;

            case 11:
                img.sprite = card11;
                ToolTip.SetDescription("Daily Coffee: \n You can stop whenever you want. Gives a Short Speed Boost");
                break;

            case 12:
                ToolTip.SetDescription("Play with fire: \n This is fine. Summon a ring of fire that will tempuarly stun other followers");
                img.sprite = card12;
                break;

            case 13:
                ToolTip.SetDescription("Occult Ritual: \nIs This safe? Increase Follower Size by 25%");
                img.sprite = card13;
                break;

            case 14:
                img.sprite = card14;
                ToolTip.SetDescription("Glasses Ritual:\nYou wear your glasses. Zooms out Camera. Will slowly go back to normal");
                break;

            case 15:
                ToolTip.SetDescription("Trip to Mage Vegas:\nThis ritual has no negative consequences... Initates a card choice");
                img.sprite = card15;
                break;
        }



    }

    


    public void cardPicked()
    {
        if (upg.IsLevelingUp == true)
        {
            switch (cardNumber)
            {
                default:
                    break;
                
                case 1: //Torch card

                    ts.torchRadius += 4;

                    break;


                
                    

                case 2: //Activate Minimap
                    Minimap.SetActive(true);
                    mapCam.enabled = true;
                    break;

                case 3: //Destory npcs only

                    player.GetComponent<PlayerMovement>().destorys = true;
                    break;

                case 4://permenent speed upgrade
                    player.GetComponent<PlayerMovement>().speed = player.GetComponent<PlayerMovement>().speed * 1.1f;
                    break;

                case 5: //Destory Bots
                    player.GetComponent<PlayerMovement>().destroyBot = true;
                    break;

                case 6: //Let other npcs in follow convert other npcs
                    player.GetComponent <PlayerMovement>().canConvert = true; 
                    break;

                case 7:
                    fourthOption.SetActive(true);
                    break;
               
                case 8: //Upgrade at noon as well as midnight
                    DCL.NoonUpgrades = true;

                    break;

                case 9: //Speed up days
                    DCL.SpeedOfTime += 0.01f;
                    break;

                case 10://Reroll Card
                    DCL.Rerolls++;
                    DCL.RerollsLeft++;
                    break;
                //RITUAL CARDS/Temp Cards
                case 11:
                    
                    rc1.CardNumber++;
                    break;

                case 12:
                    rc2.CardNumber++;
                    break;

                case 13:
                    rc3.CardNumber++;
                    break;

                case 14:
                    rc4.CardNumber++;
                    break;

                case 15:
                    rc5.CardNumber++;
                    break;
            }


        }



    }



}

