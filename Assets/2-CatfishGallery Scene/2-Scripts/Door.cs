using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Set the name of the scene you want to load
    public string nextSceneName;
    public string returningPlayerSceneName;
    int returningPlayer;

    void Start(){
       returningPlayer = PlayerPrefs.GetInt("ReturningPlayer", 0); 
    }

    // This function is called when another Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other){
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player")){
            
            // If the player is not returning, load the next scene
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
    }
}
