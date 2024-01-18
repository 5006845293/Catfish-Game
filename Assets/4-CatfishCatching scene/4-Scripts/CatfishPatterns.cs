using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Catfish
{
	earth,
	Zebra
}

public class CatfishPatterns : MonoBehaviour
{
	public Catfish Selected_Catfish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	int[] LoadIntArrayFromPlayerPrefs(){
		// Get the string representation from PlayerPrefs
		string intArrayString = PlayerPrefs.GetString("bait", "");

		// Convert the string representation back to a int array
		int[] loadedIntArray = new int[intArrayString.Length];
		for (int i = 0; i < intArrayString.Length; i++)
		{
			loadedIntArray[i] = intArrayString[i];
		}

		return loadedIntArray;
    }
}
