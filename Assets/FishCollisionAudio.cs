using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollisionAudio : MonoBehaviour
{
    public AudioSource fishCatch;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            print("Fish caught!");
        }
    }
}
