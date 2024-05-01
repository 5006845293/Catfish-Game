using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform[] spawnPoints;
	public Transform lightbound;
	public Transform middlebound;
    public GameObject player;
    public float spawnInterval = 3.0f;
    public float maxSpawnDistance = 10.0f;
	public Sprite[] fishSprites;



    private int[] activeSpawnIndices; // Array to store active spawn indices
    private int activeSpawnCount; // Number of active spawn points
    [SerializeField] private AudioClip[] randomFishNoises;
    
    private void Start()
    {
        // Initialize the array based on the maximum number of spawn points
        activeSpawnIndices = new int[spawnPoints.Length];
        activeSpawnCount = 0;

        // Start spawning fish
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			// Clear the array of active spawn indices
			System.Array.Clear(activeSpawnIndices, 0, activeSpawnIndices.Length);
			activeSpawnCount = 0;
			// Calculate the player's current Y position
			float playerY = player.transform.position.y;

			// Iterate through spawn points to identify active ones
			for (int i = 0; i < spawnPoints.Length; i++)
			{
				Transform spawnPoint = spawnPoints[i];
				float distanceToPlayer = Mathf.Abs(spawnPoint.position.y - playerY);
				// Check if the spawn point is within the maximum spawn distance
				if (distanceToPlayer <= maxSpawnDistance)
				{
					activeSpawnIndices[activeSpawnCount] = i; // Store the spawn index in the array
					activeSpawnCount++;
				}
			}

			if (activeSpawnCount > 0)
			{
				int spawnIndex = UnityEngine.Random.Range(0, activeSpawnCount);
				int spawnPointIndex = activeSpawnIndices[spawnIndex]; // Get the spawn index from the array
				Transform spawnPoint = spawnPoints[spawnPointIndex]; // Use the spawn index to access the spawn point

				GameObject newFish = Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
				Fish fishScript = newFish.GetComponent<Fish>();

				float scaleFactor = 0.25f;
				newFish.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);

				if (spawnPoint.position.x > 0)
				{
					fishScript.Selected_Direction = DirectionOptions.Left;
				}
				else
				{
					fishScript.Selected_Direction = DirectionOptions.Right;
				}

				float currentDepth = spawnPoint.transform.position.y;
				ColorOptions selectedColorOption;

				if (currentDepth >= lightbound.position.y)
				{
					selectedColorOption = (ColorOptions)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ColorOptions)).Length / 3);
				}
				else if (currentDepth >= middlebound.position.y && currentDepth <= lightbound.position.y)
				{
					selectedColorOption = (ColorOptions)UnityEngine.Random.Range(System.Enum.GetValues(typeof(ColorOptions)).Length / 3, (System.Enum.GetValues(typeof(ColorOptions)).Length / 3 * 2) + 1);
				}
				else
				{
					selectedColorOption = (ColorOptions)UnityEngine.Random.Range((System.Enum.GetValues(typeof(ColorOptions)).Length / 3 * 2) + 1, System.Enum.GetValues(typeof(ColorOptions)).Length);
				}

				fishScript.Selected_Color = selectedColorOption;

				// Set the sprite based on the selected color option
				if ((int)selectedColorOption < fishSprites.Length)
				{
					fishScript.GetComponent<SpriteRenderer>().sprite = fishSprites[(int)selectedColorOption];
				}
				else
				{
					Debug.LogError("Fish color sprite index out of range!");
				}

				AudioManager.instance.PlayRandomSound(randomFishNoises, 25);
				fishScript = null;
			}
		}
	}

}
