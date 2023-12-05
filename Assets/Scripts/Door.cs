using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Set the name of the scene you want to load
    public string nextSceneName;

    // This function is called when another Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other){
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player")){
			
            // Load the next scene when the player hits the barrier
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
