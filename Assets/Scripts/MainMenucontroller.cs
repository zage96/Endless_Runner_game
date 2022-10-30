using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenucontroller : MonoBehaviour
{
    GameObject[] panels;
    GameObject[] mmButtons;
    int maxLives = 3;

    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("subpanel");
        mmButtons = GameObject.FindGameObjectsWithTag("mmbutton");


        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }
    }

    public void ClosePanel(Button button)
    {
        button.gameObject.transform.parent.gameObject.SetActive(false);
        foreach (GameObject b in mmButtons)
        {
            b.SetActive(true);
        }
    }

    public void OpenPanel(Button button)
    {
        button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        foreach (GameObject b in mmButtons)
        {
            if(b != button.gameObject)
                b.SetActive(false);
        }
    }

    public void LoadGamescene()
    {
        PlayerController.isDead = false;
        PlayerPrefs.SetInt("lives", maxLives);
        SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);

    }

    public void LoadGameMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }

}
