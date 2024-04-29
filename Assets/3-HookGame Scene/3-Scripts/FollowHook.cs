using UnityEngine;

public class FollowHook : MonoBehaviour
{
    public Transform hook; // Reference to the hook GameObject

    void Update()
    {
        // Check if the hook reference is not null
        if (hook != null)
        {
            // Set the position of the box to match the position of the hook
            transform.position = hook.position;
        }
    }
}
