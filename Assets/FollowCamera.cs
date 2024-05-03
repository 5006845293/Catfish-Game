using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target to follow (the camera in this case)
    public Vector3 offset; // Offset from the target position

    void Update()
    {
        if (target != null)
        {
            // Calculate the desired position based on the target's position and offset
            Vector3 targetPosition = target.position + offset;

            // Set the object's position to the desired position
            transform.position = targetPosition;
        }
    }
}
