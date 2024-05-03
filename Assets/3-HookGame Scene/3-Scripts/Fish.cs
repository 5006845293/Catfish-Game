using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorOptions
{
    Black,
    White,
    Red,
    Yellow,
    Green,
    Blue,
    Purple
}

public enum DirectionOptions
{
    Right,
    Left
}

public class Fish : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;

    public ColorOptions Selected_Color;
    public DirectionOptions Selected_Direction;
    public float swimSpeed = 7.0f;
    public float sine = 7.0f;
    public float fadeSpeed = 0.5f; // Speed at which the fish fades in/out
    private bool fadingOut = false;
	private float verticalSineValue;
	
	
    // Start is called before the first frame update
	void Start()
	{
		// Initialize sprite renderer
		spriteRenderer = GetComponent<SpriteRenderer>();


		// Start the fade out process for the purple fish
		if (Selected_Color == ColorOptions.Purple)
		{
			fadingOut = true;
		}
	}





    // Update is called once per frame
    void Update()
    {
		
		// Check if fish is out of bounds for destruction
		if (transform.position.x > 23f || transform.position.x < -20f)
		{
			Destroy(gameObject); // Destroy the fish object
		}
		
        // Check if the fish is black
        if (Selected_Color == ColorOptions.Black)
		{
			verticalSineValue = sine * Mathf.Sin(Time.time);
			// Move black fish in the selected direction horizontally
			if (Selected_Direction == DirectionOptions.Right)
			{
				transform.Translate(Vector3.right * sine * Time.deltaTime);
				spriteRenderer.flipX = true;
			}
			else if (Selected_Direction == DirectionOptions.Left)
			{
				transform.Translate(Vector3.left * sine * Time.deltaTime);
				spriteRenderer.flipX = false;
			}

			// Apply the precalculated vertical movement
			transform.Translate(Vector3.up * verticalSineValue * Time.deltaTime);
		}
		else
		{
			// Move non-black fish in the selected direction
			if (Selected_Direction == DirectionOptions.Right)
			{
				transform.Translate(Vector3.right * swimSpeed * Time.deltaTime);
				spriteRenderer.flipX = true;
			}
			else if (Selected_Direction == DirectionOptions.Left)
			{
				transform.Translate(Vector3.left * swimSpeed * Time.deltaTime);
				spriteRenderer.flipX = false;
			}
		}

        // If the fish is purple and fading out, decrease its alpha value
        if (Selected_Color == ColorOptions.Purple && fadingOut)
        {
            Color currentColor = spriteRenderer.color;
            float newAlpha = currentColor.a - fadeSpeed * Time.deltaTime;
            if (newAlpha <= 0f)
            {
                newAlpha = 0f;
                fadingOut = false; // Stop fading out when alpha reaches 0
            }
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
        // If the fish is purple and not fading out, increase its alpha value
        else if (Selected_Color == ColorOptions.Purple && !fadingOut)
        {
            Color currentColor = spriteRenderer.color;
            float newAlpha = currentColor.a + fadeSpeed * Time.deltaTime;
            if (newAlpha >= 1f)
            {
                newAlpha = 1f;
                fadingOut = true; // Start fading out when alpha reaches 1
            }
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
    }

    
}
