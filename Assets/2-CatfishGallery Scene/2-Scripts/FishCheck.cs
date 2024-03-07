using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCheck : MonoBehaviour
{
	public GameObject[] Fish; 
	
    // Start is called before the first frame update
    void Start()
    {
		bool[] loadedBoolArray = LoadBoolArrayFromPlayerPrefs();
		Debug.LogWarning("Bool array " + loadedBoolArray);
		for (int i = 0; i < Fish.Length; i++){
			SpriteRenderer spriteRenderer = Fish[i].GetComponent<SpriteRenderer>();
			spriteRenderer.color = loadedBoolArray[i] ? Color.white : Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	bool[] LoadBoolArrayFromPlayerPrefs(){
		// Get the string representation from PlayerPrefs
		string boolArrayString = PlayerPrefs.GetString("SavedFish", "");

		// Convert the string representation back to a bool array
		bool[] loadedBoolArray = new bool[boolArrayString.Length];
		for (int i = 0; i < boolArrayString.Length; i++)
		{
			loadedBoolArray[i] = boolArrayString[i] == '1';
		}

		return loadedBoolArray;
    }
}
