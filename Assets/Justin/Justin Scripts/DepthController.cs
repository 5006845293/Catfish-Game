using UnityEngine;
using TMPro; // Make sure to add this if you are using TextMeshPro for your text display

public class DepthController : MonoBehaviour
{
    public TextMeshProUGUI depthText;
    private float currentDepth;

    
    void Update()
    {
		
		currentDepth = transform.position.y * -6.85f;
		if(currentDepth >= 500){
				currentDepth = 500f;
		}
		// Update depth text
		UpdateDepthText();
	
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
