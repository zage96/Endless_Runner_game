using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMusic : MonoBehaviour
{

    List<AudioSource> music = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] allAS = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();
        music.Add(allAS[0]);
    }
    
    public void UpdateMusicVolume(float value)
    {
        foreach(AudioSource m in music)
        {
            m.volume = value;
        }
    }
}
