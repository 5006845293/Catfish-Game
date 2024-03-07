using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UpdateCatfish : MonoBehaviour
{
	public string catfish;
	public TMP_Text fishtext;
	private SpriteRenderer spriteRenderer;
	private Texture2D texture;
	public bool[] MaskBool = new bool[12];
	
    // Start is called before the first frame update
    void Start()
    {
		catfish = PlayerPrefs.GetString("Catfish"); //water Catfish grab the saved catfish
		
		//change the text to the appropriate text
		fishtext.text ="Congrats!You've caught a " + catfish +"!";
		
		//set the image to the corresponding catfish
		spriteRenderer = GetComponent<SpriteRenderer>();
		string fileName = "CatFish_" + (catfish.Split(' ')[0]) + ".png"; 
		string filePath = "Assets/3-HookGame Scene/3-Textures/Fish/Catfish/" + fileName;
		texture = LoadTextureFromFile(filePath);
		if (texture != null){
            // Create a sprite from the loaded texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Set the sprite to the Sprite Renderer
            spriteRenderer.sprite = sprite;
        }
		
		//save the fish to the gallery
		switch (catfish)
        {
            case "Zebra Catfish":
                Debug.Log("Zebra Catfish added to the gallery");
				ConvertIntToBoolArray("100000000000",ref MaskBool);
                break;
            case "Rainbow Catfish":
                Debug.Log("Rainbow Catfish added to the gallery");
				ConvertIntToBoolArray("010000000000",ref MaskBool);
                break;
            case "Fire Catfish":
                Debug.Log("Fire Catfish added to the gallery");
				ConvertIntToBoolArray("001000000000",ref MaskBool);
                break;
			case "Earth Catfish":
                Debug.Log("Earth Catfish added to the gallery");
				ConvertIntToBoolArray("000100000000",ref MaskBool);
                break;
            case "Air Catfish":
                Debug.Log("Air Catfish added to the gallery");
				ConvertIntToBoolArray("000010000000",ref MaskBool);
                break;
            case "Water Catfish":
                Debug.Log("Water Catfish added to the gallery");
				ConvertIntToBoolArray("000001000000",ref MaskBool);
                break;
            case "Spring Catfish":
                Debug.Log("Spring Catfish added to the gallery");
				ConvertIntToBoolArray("000000100000",ref MaskBool);
                break;
            case "Winter Catfish":
                Debug.Log("Winter Catfish added to the gallery");
				ConvertIntToBoolArray("000000010000",ref MaskBool);
                break;
            case "Fall Catfish":
                Debug.Log("Fall Catfish added to the gallery");
				ConvertIntToBoolArray("000000001000",ref MaskBool);
                break;
			case "Summer Catfish":
                Debug.Log("Summer Catfish added to the gallery");
				ConvertIntToBoolArray("000000000100",ref MaskBool);
                break;
            case "Love Catfish":
                Debug.Log("Love Catfish added to the gallery");
				ConvertIntToBoolArray("000000000010",ref MaskBool);
                break;
            case "Hate Catfish":
                Debug.Log("Hate Catfish added to the gallery");
				ConvertIntToBoolArray("000000000001",ref MaskBool);
                break;
            default:
                Debug.Log("Basic catfish nothing added to the gallery");
				ConvertIntToBoolArray("000000000000",ref MaskBool);
                break;
        }
		Debug.Log("Maskbool: " + string.Join(", ",MaskBool));
		Debug.Log("final new fish array: " + string.Join(", ", BitwiseOR(LoadBoolArrayFromPlayerPrefs(), MaskBool)));

		SaveBoolArrayToPlayerPrefs(BitwiseOR(LoadBoolArrayFromPlayerPrefs(), MaskBool));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Gallery");
        }
    }

	private Texture2D LoadTextureFromFile(string path) {
		Texture2D tex = new Texture2D(2, 2);
		byte[] fileData = null;
		try {
			fileData = System.IO.File.ReadAllBytes(path);
		} catch (System.Exception e) {
			Debug.LogWarning("File not found: " + e.Message);
		}

		if (fileData != null) {
			tex.LoadImage(fileData);
		} else {
			// Load default image
			string defaultPath = "Assets/3-HookGame Scene/3-Textures/Fish/Catfish/Catfish_Earth.png";
			try {
				fileData = System.IO.File.ReadAllBytes(defaultPath);
				tex.LoadImage(fileData);
			} catch (System.Exception ex) {
				Debug.LogError("Default image not found: " + ex.Message);
			}
		}

		return tex;
	}
	
	
	public bool[] BitwiseOR(bool[] arrayA, bool[] arrayB)
	{
		Debug.Log("bitmask: " + string.Join(", ",arrayB));
		int maxLength = Mathf.Max(arrayA.Length, arrayB.Length); // Determine the longer array's length
		
		bool[] paddedArrayA = new bool[maxLength];
		bool[] paddedArrayB = new bool[maxLength];

		// Copy elements from arrayA, pad with zeros if necessary
		for (int i = 0; i < maxLength; i++)
		{
			if (i < arrayA.Length)
				paddedArrayA[i] = arrayA[i];
			else
				paddedArrayA[i] = false;
		}

		bool[] result = new bool[maxLength];
		for (int i = 0; i < maxLength; i++)
		{
			result[i] = paddedArrayA[i] || arrayB[i];
		}
		return result;
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
	
	void ConvertIntToBoolArray(string num, ref bool[] boolArray)
	{
		// Ensure boolArray has enough length to hold the binary string
		if (boolArray.Length < num.Length)
		{
			Array.Resize(ref boolArray, num.Length);
		}

		for (int i = 0; i < num.Length; i++)
		{
			boolArray[i] = num[i] == '1'; // Set each element of the array based on binary representation
		}
	}


}
