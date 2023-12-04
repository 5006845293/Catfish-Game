using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab; // Reference to the fish prefab
    public float spawnInterval = 3.0f; // Time interval between spawns
    public Transform[] spawnPoints; // Array of spawn points for fish

    private void Start()
    {
        // Start spawning fish
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Choose a random spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // Instantiate a fish at the chosen spawn point
            GameObject newFish = Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);

            // Set the properties of the spawned fish (color, direction, etc.) - You can access Fish script here if needed
            Fish fishScript = newFish.GetComponent<Fish>();
			// Check the spawn point's position on the screen
			Debug.LogError("Position half " + Screen.width);
			Debug.LogError("Position of spawn " + spawnPoint.position.x);
			if (spawnPoint.position.x > 0) // Spawning on the right side of the screen
			{
				fishScript.Selected_Direction = DirectionOptions.Left; // Swim left
			}
			else // Spawning on the left side of the screen
			{
				fishScript.Selected_Direction = DirectionOptions.Right; // Swim right
			}

			fishScript.Selected_Color = (ColorOptions)Random.Range(0, System.Enum.GetValues(typeof(ColorOptions)).Length);
        }
    }
}
