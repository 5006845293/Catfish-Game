using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
	public Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check tag of object colliding
        if (collision.gameObject.CompareTag("Barrier"))
        {
            // Prevent the player from moving through the barrier
            StopPlayerMovement();

            Debug.Log("Player collided with an obstacle!");

        }
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * speed * Time.deltaTime;

        // Calculate boundaries of the visible area (change according to your scene)
        float minX = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float minY = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, mainCamera.nearClipPlane)).y;
        float maxY = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, mainCamera.nearClipPlane)).y;

        // Move the player
        transform.Translate(movement);

        // Restrict player movement within the visible area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;

    }

    void StopPlayerMovement()
    {
        // stop the player's Rigidbody velocity
        rb.velocity = new Vector2(0, 0);
    }
    


}


