using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("ReturningPlayer", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
