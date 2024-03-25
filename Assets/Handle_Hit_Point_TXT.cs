using UnityEngine;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class Handle_Hit_Point_TXT : MonoBehaviour
{
    public TextMeshProUGUI textObject, score;
    private PlayerMovement player;
    private float textNum;

    private void Start() {
        player = GetComponent<PlayerMovement>();
    }

    private void Update() {
        textNum = (float)Math.Floor(player.hit_points/10f);
        ChangeLives(string.Format($"# of LIVES = {textNum}"));
        ChangeScore("Score =" +player.scoreValue);
    }
    public void ChangeLives(string newText)
    {
        if (textObject != null)
        {
            textObject.text = newText;
        }
    }
    public void ChangeScore(string newText)
    {
        if(score != null){
            score.text = newText;
        }
    }
}
