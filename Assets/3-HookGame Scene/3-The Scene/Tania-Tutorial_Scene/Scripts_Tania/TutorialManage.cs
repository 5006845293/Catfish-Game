using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManage : MonoBehaviour
{
    public Transform[] targetObjects; // Array of target objects to move towards
    public float moveSpeed = 5f;      // Speed of the camera movement

    private int currentTargetIndex = 0; // Index of the current target object

    void Start()
    {
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
        // Move the camera towards the current target object
        MoveCamera();
    }

    void MoveCamera()
    {
        // Calculate the direction towards the current target object
        Vector3 direction = targetObjects[currentTargetIndex].position - transform.position;

        // Move towards the current target object
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        // Check if the camera has reached the current target object
        if (Vector3.Distance(transform.position, targetObjects[currentTargetIndex].position) < 0.1f) // Adjust the threshold as needed
        {
            // Check if there are more targets
            if (currentTargetIndex < targetObjects.Length - 1)
            {
                currentTargetIndex++; // Move to the next target
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
        // Load the next scene
        SceneManager.LoadScene(4);
    }
}
