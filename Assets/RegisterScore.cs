using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RegisterScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameData.singleton.scoreText = this.GetComponent<TMP_Text>();
        GameData.singleton.UpdateScore(0);
    }


}
