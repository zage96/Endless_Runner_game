using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameData : MonoBehaviour
{

    public static GameData singleton;
    public TMP_Text scoreText = null;
    public GameObject musicSlider;
    public GameObject soundSlider;
    int score = 0;

    void Awake()
    {
        GameObject[] gd = GameObject.FindGameObjectsWithTag("gamedata");
        if(gd.Length>1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        singleton = this;

        PlayerPrefs.SetInt("score", 0);

        musicSlider.GetComponent<UpdateMusic>().Start();
        musicSlider.GetComponent<UpdateSound>().Start();
    }


    public void UpdateScore(int s)
    {
        score += s;
        PlayerPrefs.SetInt("score", score);
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

}
