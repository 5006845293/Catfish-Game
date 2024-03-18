using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	
	    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call your function here
            StartGame();
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("ReturningPlayer", 0);
        PlayerPrefs.Save();
		bool[] boolArray = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false};
		SaveBoolArrayToPlayerPrefs(boolArray);
        SceneManager.LoadScene(1);
    }
	
	void SaveBoolArrayToPlayerPrefs(bool[] array){
        // Convert the bool array to a string representation
        string boolArrayString = "";
        for (int i = 0; i < array.Length; i++)
        {
            boolArrayString += array[i] ? "1" : "0";
        }

        // Save the string representation to PlayerPrefs
        PlayerPrefs.SetString("SavedFish", boolArrayString);
        PlayerPrefs.Save();
    }
	
}
