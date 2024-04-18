using UnityEngine;
using TMPro; 

//depth start = -3.22
//depth 200 = -29.14856
//depth 400 = -58.35699
//depth 500 = -72.9567

public class DepthController : MonoBehaviour
{
    public TextMeshProUGUI depthText;
    private float currentDepth;
    public float speed;
    
    
    void Update()
    {

        currentDepth = transform.position.y * speed;
        
        speed = -10f;
        
		if(currentDepth >500){
			currentDepth=500;
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
