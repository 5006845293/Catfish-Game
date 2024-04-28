using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class HookPlayerMovement : MonoBehaviour
{
    public float speed;
    public Camera mainCamera;
private bool canMove = true; // Flag to control movement

    void Update()
    {
        if (canMove)
        {
            // Get input for player movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calculate movement vector
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * speed * Time.deltaTime;

            // Move the player
            transform.Translate(movement);

            // Calculate boundaries of the visible area
            float minX = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            float minY = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

            // Restrict player movement within the visible area
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            transform.position = clampedPosition;
        }
    }

    public void FreezeMovement()
    {
        canMove = false; // Disable movement
    }

    public void UnfreezeMovement()
    {
        canMove = true; // Enable movement
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            // If the player collides with a barrier, prevent it from passing through
            Vector2 movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.Translate(-movementDirection * speed * Time.deltaTime);
        }
    }
}