using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private FishingLine line;
    // Start is called before the first frame update
    void Start()
    {
        line.SetUpLine(points);
    }
   
}


//basic catfish i can do better
//minimap more visible
//tell them how low hook goes
//differebt types of baitfish
//positive feedback/stats for catfish catch
//endgame lore
//animations + more features
//catfish catch minigame?
//add challenging things
//power ups depending on catfish