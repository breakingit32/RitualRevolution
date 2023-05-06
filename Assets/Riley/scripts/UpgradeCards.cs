using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCards : MonoBehaviour
{
    public int cardNumber = 0;
    public Upgrades upg;
    public Sprite card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12, card13, card14, card15;
    public RitualLogic rc1, rc2, rc3, rc4, rc5;
    private Image img;
    public TorchSizer ts;
    public GameObject Minimap;
    public DayCycleLogic DCL;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
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

            case 1:
                img.sprite = card1;
                break;

            case 2:
                img.sprite = card2;
                break;

            case 3:
                img.sprite = card3;
                break;

            case 4:
                img.sprite = card4;
                break;

            case 5:
                img.sprite = card5;
                break;

            case 6:
                img.sprite = card6;
                break;

            case 7:
                img.sprite = card7;
                break;

            case 8:
                img.sprite = card8;
                break;

            case 9:
                img.sprite = card9;
                break;

            case 10:
                img.sprite = card10;
                break;

            case 11:
                img.sprite = card11;
                break;

            case 12:
                img.sprite = card12;
                break;

            case 13:
                img.sprite = card13;
                break;

            case 14:
                img.sprite = card14;
                break;

            case 15:
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

                case 1: //Torch Card
                    ts.torchRadius += 2;
                    break;

                case 2://Minimap Card
                    Minimap.SetActive(true);
                    break;

                case 3: //Reroll Card
                    DCL.Rerolls++;
                    DCL.RerollsLeft++;
                    break;

                case 4:
                    
                    break;

                case 5:
                    
                    break;

                case 6:
                    
                    break;

                case 7:
                    
                    break;

                case 8:
                    
                    break;

                case 9:
                    
                    break;

                case 10:
                    
                    break;

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
