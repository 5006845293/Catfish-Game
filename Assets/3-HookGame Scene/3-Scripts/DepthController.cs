using UnityEngine;
using TMPro; 

public class DepthController : MonoBehaviour
{
    public TextMeshProUGUI depthText;
    private float currentDepth;
    public float speed;
    
    
    void Update()
    {

        currentDepth = transform.position.y * speed;
        
        if(currentDepth <= 250)
        {
            speed = -5.85f;
        }
        
        else
        {
            speed = -6.85f;
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
	
	public float GetCurrentDepth()
	{
		return currentDepth;
	}

}
