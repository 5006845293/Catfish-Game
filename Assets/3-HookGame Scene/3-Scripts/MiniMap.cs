using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Transform hook;
    public RawImage rawImage;
    public float mapScale = 0.1f;

    void LateUpdate()
    {
        Vector2 newPosition = hook.position * mapScale;
        rawImage.uvRect = new Rect(newPosition.x, newPosition.y, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}



