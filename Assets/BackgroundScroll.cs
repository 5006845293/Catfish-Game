using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float verticalSpeed = 0.2f; //controls how fast the background scrolls
    private Renderer re;

    void Start()
    {
        re = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.deltaTime * verticalSpeed);
        re.material.mainTextureOffset = offset; //every frame moves material by changing its offset
        Debug.Log("scroll");
    }
}
