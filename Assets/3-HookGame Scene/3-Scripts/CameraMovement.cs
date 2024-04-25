using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeedUp = 2.0f;   // Speed for going up
    public float moveSpeedDown = 1.0f; // Speed for going down
    public float pauseDuration = 2.0f; // Duration to pause at the bottom position
	public Transform endPosition;
	
    private Vector3 startPosition;
    private float timer = 0.0f;
    private bool movingDown = true;
    private DepthController depthController;

    void Start()
    {
        startPosition = transform.position;
        depthController = GetComponent<DepthController>(); // Get the DepthController component
    }

    void Update()
    {
        float currentDepth = depthController.GetCurrentDepth(); // Get the current depth from DepthController

        if (movingDown)
        {
            Vector3 targetPositionDown = new Vector3(-1.7f,endPosition.position.y,0.0f);
            float step = moveSpeedDown * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPositionDown, step);
			
            if (currentDepth >= 500f)
            {
                movingDown = false;
				Debug.Log(movingDown);
                timer = 0.0f; // Reset timer for pause duration
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= pauseDuration)
            {
				
                float step = moveSpeedUp * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, step);

            }
        }
    }
}
