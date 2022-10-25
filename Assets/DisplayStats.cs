using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DisplayStats : MonoBehaviour
{
    public TMP_Text lastScore;
    public TMP_Text highestScore;

    // GameData.singleton.scoreText = this.GetComponent<TMP_Text>();

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("lastscore"))
        {
            lastScore.text = "Last Score: " + PlayerPrefs.GetInt("lastscore");
        }
        else
        {
            lastScore.text = "Last Score: 0";
        }

        if (PlayerPrefs.HasKey("highscore"))
        {
            highestScore.text = "Highest Score: " + PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highestScore.text = "Highest Score: 0";
        }
    }
}
