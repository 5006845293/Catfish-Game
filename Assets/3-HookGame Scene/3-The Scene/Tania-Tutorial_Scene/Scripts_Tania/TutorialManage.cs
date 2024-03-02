using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManage : MonoBehaviour
{
    public Transform[] targetObjects; // Array of target objects to move towards
    public float moveSpeed = 5f;      // Speed of the camera movement

    private int currentTargetIndex = 0; // Index of the current target object
    private bool isMoving = false;       // Flag to check if camera is currently moving

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0f;

        // Check if any target objects are assigned
        if (targetObjects == null || targetObjects.Length == 0)
        {
            Debug.LogError("No target objects assigned!");
            enabled = false; // Disable the script if no targets are assigned
        }
        else
        {
            // Set the initial position of the camera to match the position of the first target object
            transform.position = targetObjects[0].position;
        }
    }

    void Update()
    {
        // Check if the camera is not moving and the game is paused
        if (!isMoving && Time.timeScale == 0)
        {
            // Move the camera towards the current target object
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        // Calculate the direction towards the current target object
        Vector3 direction = targetObjects[currentTargetIndex].position - transform.position;

        // Calculate the distance to the current target object
        float distance = direction.magnitude;

        // Normalize the direction vector
        direction.Normalize();

        // Move towards the current target object
        transform.Translate(direction * moveSpeed * Time.unscaledDeltaTime);

        // Check if the camera has reached the current target object
        if (distance < 0.1f) // Adjust the threshold as needed
        {
            // Stop moving if the camera is close enough to the current target
            isMoving = true;

            // Check if there are more targets
            if (currentTargetIndex < targetObjects.Length - 1)
            {
                currentTargetIndex++; // Move to the next target
                isMoving = false; // Reset the flag to allow moving towards the next target
            }
            else
            {
                // If all targets are reached, load the next scene
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(4);
    }
}