using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.SceneManagement;

public class ReaderQR : MonoBehaviour
{
    private WebCamTexture camTexture;
    private Rect screenRect;

    void Start()
    {
        InitializeWebcam();
    }

    void InitializeWebcam()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            // Release any existing webcam texture before initializing a new one
            ReleaseWebcam();

            camTexture = new WebCamTexture();
            camTexture.requestedHeight = Screen.height;
            camTexture.requestedWidth = Screen.width;
            camTexture.Play();

            screenRect = new Rect(0, 0, Screen.width, Screen.height);

            Debug.Log("Webcam initialized successfully.");
        }
        else
        {
            Debug.LogError("No webcam available or permission denied.");
        }
    }

    void ReleaseWebcam()
    {
        if (camTexture != null)
        {
            camTexture.Stop();
            Destroy(camTexture);
            camTexture = null;

            Debug.Log("Webcam released.");
        }
    }

    void Update()
    {
        if (camTexture != null && camTexture.isPlaying)
        {
            Graphics.DrawTexture(screenRect, camTexture);
            ScanQRCode();
        }
        else
        {
            Debug.LogWarning("Webcam not running or texture not playing.");
        }
    }

    void ScanQRCode()
	{
		try
		{
			Color32[] pixels = camTexture.GetPixels32();
			BarcodeReader reader = new BarcodeReader();
			Result result = reader.Decode(pixels, camTexture.width, camTexture.height);

			if (result != null)
			{
				// Check if the scanned QR code contains only 1s and 0s
				if (IsBinaryString(result.Text)&& result.Text.Length>=12)
				{
					Debug.Log("Valid QR Code Scanned: " + result.Text);
					// Trigger your desired action based on the scanned QR code data
					PlayerPrefs.SetString("SavedFish", result.Text);
					PlayerPrefs.Save();
					SceneManager.LoadScene("Gallery");
				}
				else
				{
					Debug.LogWarning("Invalid QR Code. Only binary strings (1s and 0s) at length 12+ are accepted.");
				}
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogError("Error scanning QR code: " + ex.Message);
		}
	}

	bool IsBinaryString(string input)
	{
		foreach (char c in input)
		{
			if (c != '0' && c != '1')
			{
				return false;
			}
		}
		return true;
	}

    void OnGUI()
    {
        // Optionally, add GUI elements or feedback for scanning
    }

    void OnDestroy()
    {
        // Make sure to release the webcam texture when the script or scene is destroyed
        ReleaseWebcam();
    }
}
