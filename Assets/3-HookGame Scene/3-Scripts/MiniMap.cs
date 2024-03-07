using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Transform hook;
    public Image image;
    public float mapScale = 0.1f;

    void LateUpdate()
    {
        Vector2 newPosition = hook.position * mapScale;
        image.rectTransform.anchoredPosition = newPosition;
    }
}



