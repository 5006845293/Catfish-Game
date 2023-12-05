using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float pauseDuration = 2.0f; // Duration to pause at the bottom position
    public float moveDistance = 5.0f; // Total distance to move

    private Vector3 startPosition;
    private float timer = 0.0f;
    private bool movingDown = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingDown)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition - Vector3.up * moveDistance, step);

            if (transform.position.y <= startPosition.y - moveDistance)
            {
                movingDown = false;
                timer = 0.0f; // Reset timer for pause duration
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= pauseDuration)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, step);

                if (transform.position.y >= startPosition.y)
                {
                    movingDown = true;
                    timer = 0.0f; // Reset timer for next downward movement
                }
            }
        }
    }
}
