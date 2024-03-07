using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishSetter : MonoBehaviour
{
	
	public Toggle toggle1;
    public Toggle toggle2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
	public void continueGame(){
		bool[] boolArray = new bool[12];
		boolArray[0] = toggle1.isOn;
        boolArray[1] = toggle2.isOn;
		SaveBoolArrayToPlayerPrefs(boolArray);
	}
	public void NewGame(){
		bool[] boolArray = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false};
		SaveBoolArrayToPlayerPrefs(boolArray);
	}
}
