using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    // Set the name of the scene you want to load
    public string nextSceneName;
    public string returningPlayerSceneName;
    int returningPlayer;
    public GameObject DoorMenu;
    public GameObject ActualQRCode;

    //QR Stuff
    [SerializeField]
    private RawImage _rawImageReceiver;
    private Texture2D _storeEncodedTexture;
    void Start(){
       returningPlayer = PlayerPrefs.GetInt("ReturningPlayer", 0);
       _storeEncodedTexture = new Texture2D(256, 256);
    }

	    void Update()
    {
		if(DoorMenu.activeSelf==true){
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				PlayerClicksPlay();
			}
			else if(Input.GetKeyDown(KeyCode.Space))
			{
				PlayerClicksQuit();
			}
		}
    }
	
    // This function is called when another Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other){
		
		
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player")){
			Time.timeScale = 0f;
            if(returningPlayer == 0)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                DoorMenu.SetActive(true);
                
            }
        }
    }

    public void PlayerClicksPlay()
    {
        if (returningPlayer == 1)
        {
            // If the player is returning, load the returning player scene
            SceneManager.LoadScene(returningPlayerSceneName);

        }
        else
        {
            // Load the next scene when the player hits the barrier
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void PlayerClicksQuit()
    {
        Debug.Log("This is when QR code generator functions are called");
        EncodeTextToQRCode();
        DoorMenu.SetActive(false);
        ActualQRCode.SetActive(true);
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

	public void EncodeTextToQRCode()
	{
		string boolArrayString = PlayerPrefs.GetString("SavedFish", "");
		Color32[] convertPixelToTexture = Encode(boolArrayString, _storeEncodedTexture.width, _storeEncodedTexture.height);
		
		// Iterate through the color array and set black pixels to be transparent
		for (int i = 0; i < convertPixelToTexture.Length; i++)
		{
			if (convertPixelToTexture[i].r == 0 && convertPixelToTexture[i].g == 0 && convertPixelToTexture[i].b == 0)
			{
				convertPixelToTexture[i].a = 0; // Set alpha (transparency) to 0 for black pixels
			}
		}

		// Apply the modified color array to the texture
		_storeEncodedTexture.SetPixels32(convertPixelToTexture);
		_storeEncodedTexture.Apply();

		// Set the modified texture with transparent black pixels to the RawImage component
		_rawImageReceiver.texture = _storeEncodedTexture;
	}


}
