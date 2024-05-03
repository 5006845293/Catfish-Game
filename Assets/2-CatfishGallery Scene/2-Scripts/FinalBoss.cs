using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public GameObject finalBossBarrier;
    public GameObject finalBossRoom;
    private SpriteRenderer roomSpriteRenderer;

    void Start()
    {
        roomSpriteRenderer = finalBossRoom.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the saved catfish array from PlayerPrefs
            string boolArrayString = PlayerPrefs.GetString("SavedFish", "");

            // Check if all catfish are collected (set to true)
            if (AllCatfishCollected(boolArrayString))
            {
                // Deactivate the final boss barrier
                if (finalBossBarrier != null)
                {
                    finalBossBarrier.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Final boss barrier is not assigned!");
                }

                // Restore room visibility
                if (roomSpriteRenderer != null)
                {
                    roomSpriteRenderer.color = Color.white; // Reset color to default (visible)
                }
                else
                {
                    Debug.LogWarning("Room SpriteRenderer is not assigned!");
                }
            }
            else
            {
                Debug.LogWarning("Player hasn't collected all catfish!");
                
            }
        }
    }

    // Function to check if all catfish are collected
    bool AllCatfishCollected(string boolArrayString)
    {
        for (int i = 0; i < boolArrayString.Length; i++)
        {
            if (boolArrayString[i] == '0')
            {
                return false; // If any catfish is not collected (set to false), return false
            }
        }
        return true; // All catfish are collected (set to true)
    }
}
