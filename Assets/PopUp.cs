using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject PopUpMenu;
    public static bool PopUpActive;

    void Start()
    {
        PopUpActive = true;
        PopUpMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopUpActive)
            {
                Resume();
            }
            else
            {
                Popup();
            }
        }
    }

    void Resume()
    {
        PopUpMenu.SetActive(false);
        PopUpActive = false;
        Time.timeScale = 1f;
    }

    void Popup()
    {
        PopUpMenu.SetActive(true);
        PopUpActive = true;
        Time.timeScale = 0f;
    }
}
