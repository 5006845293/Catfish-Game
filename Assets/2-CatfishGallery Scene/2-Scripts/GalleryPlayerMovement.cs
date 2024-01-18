using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f; // Adjust the movement speed as needed

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the input
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        // Apply the movement to the player's position
        transform.Translate(movement);
    }
}
