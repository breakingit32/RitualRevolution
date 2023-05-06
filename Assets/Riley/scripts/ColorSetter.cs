using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour
{
    public Slider SlRed;
    public Slider SlBlue;
    public Slider SlGreen;
    public Image preview;
    public Color FollowingColor = Color.white;
    public SpriteRenderer Player;
    public SpriteRenderer PlayerIcon;
    public Sprite playerDialogue;
    public Image Avatar;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Avatar.sprite == playerDialogue) Avatar.color = FollowingColor;
        else Avatar.color = Color.white;
    }

    public void AlterColor()
    {
        FollowingColor = new Color(SlRed.value, SlGreen.value, SlBlue.value);
        preview.color = FollowingColor;
        Player.color = FollowingColor;
        PlayerIcon.color = FollowingColor;
    }

}
