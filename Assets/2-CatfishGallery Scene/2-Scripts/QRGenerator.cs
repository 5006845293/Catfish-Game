using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QRGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImageReceiver;

    private Texture2D _storeEncodedTexture;

    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
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

        // Encode the text and set white color with transparent background
        var encoded = writer.Write(textForEncoding);
        for (int i = 0; i < encoded.Length; i++)
        {
            encoded[i] = new Color32(255, 255, 255, encoded[i].a);
        }

        return encoded;
    }

    public void EncodeTextToQRCode()
    {
        string boolArrayString = PlayerPrefs.GetString("SavedFish", "");
        Color32[] convertPixelToTexture = Encode(boolArrayString, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(convertPixelToTexture);
        _storeEncodedTexture.Apply();

        _rawImageReceiver.texture = _storeEncodedTexture;
    }
}
