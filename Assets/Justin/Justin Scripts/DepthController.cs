using UnityEngine;
using TMPro; // Make sure to add this if you are using TextMeshPro for your text display

public class DepthController : MonoBehaviour
{
    public float maxDepth = 500f;
    public float minDepth = 0f;
    public float depthIncrement = 1f;

    public TextMeshProUGUI depthText;

    private float currentDepth = 0f;

    
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); // Get input for vertical movement

        if (verticalInput < 0)
        {
            // Player is moving down
            currentDepth += depthIncrement * Time.deltaTime;
            currentDepth = Mathf.Clamp(currentDepth, minDepth, maxDepth);

            // Update depth text
            UpdateDepthText();
        }
        else if (verticalInput > 0)
        {
            // Player is moving up
            currentDepth -= depthIncrement * Time.deltaTime;
            currentDepth = Mathf.Clamp(currentDepth, minDepth, maxDepth);

            // Update depth text
            UpdateDepthText();
        }
    }

    void UpdateDepthText()
    {
        // Update the depth text component with the current depth value
        if (depthText != null)
        {
            depthText.text = $"Depth: {Mathf.RoundToInt(currentDepth)} m";
        }
    }
}
