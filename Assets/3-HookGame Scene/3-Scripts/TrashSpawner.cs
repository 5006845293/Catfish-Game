using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
	public GameObject trashPrefab;
    public Transform[] spawnPoints;
    public GameObject player;
    public float spawnInterval = 3.0f;
    public float maxSpawnDistance = 10.0f;

    private int[] activeSpawnIndices; // Array to store active spawn indices
    private int activeSpawnCount; // Number of active spawn points

    private void Start()
    {
        // Initialize the array based on the maximum number of spawn points
        activeSpawnIndices = new int[spawnPoints.Length];
        activeSpawnCount = 0;

        // Start spawning trash
        StartCoroutine(SpawnTrash());
    }

    IEnumerator SpawnTrash()
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
				
                int spawnIndex = Random.Range(0, activeSpawnCount);
                int spawnPointIndex = activeSpawnIndices[spawnIndex]; // Get the spawn index from the array

                Transform spawnPoint = spawnPoints[spawnPointIndex]; // Use the spawn index to access the spawn point

                GameObject newTrash= Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);
                Trash trashScript = newTrash.GetComponent<Trash>();
                float scaleFactor = 0.25f;
                newTrash.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);

                if (spawnPoint.position.x > 0)
                {
                    trashScript.Selected_Direction = DirectionOptions.Left;
                }
                else
                {
                    trashScript.Selected_Direction = DirectionOptions.Right;
                }

                    trashScript.Selected_Type = (TypeOptions)Random.Range(0, System.Enum.GetValues(typeof(TypeOptions)).Length);
                
            }
        }
    }
}
