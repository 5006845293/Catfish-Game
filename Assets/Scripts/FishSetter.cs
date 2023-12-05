using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool[] boolArray = new bool[] { true, false};
		SaveBoolArrayToPlayerPrefs(boolArray);
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
}
