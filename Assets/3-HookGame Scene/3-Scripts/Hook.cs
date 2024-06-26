using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform fishHolder;  
    public int maxFishCount = 5;
    public DepthController DepthScript;
    public bool isHooking = true;
    public List<GameObject> fishesOnHook = new List<GameObject>();
    [SerializeField] private AudioClip fishCatch;
	public GameObject objectTypeToFind;
	public GameObject[] objectsOfType;
    private Dictionary<GameObject, float> originalFishSpeeds = new Dictionary<GameObject, float>();
	private Fish fishController;
	public HookPlayerMovement hookPlayerMovement;
	public UnityEngine.U2D.SpriteShapeRenderer [] Fishrenderer;
	public GameObject lightningEffect;
    public Transform hook; 

    // Variable to keep track of collected fishes.
    private int fishCount = 0;

    // UI text component to display count of fishes collected.
    public TextMeshProUGUI fishCountText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        isHooking = true;
		

        // Initialize fishCount to zero.
        fishCount = 0;

        // Update the fish count display.
        SetFishCountText();
    }

    private void FixedUpdate()
    {
        if (DepthScript.GetCurrentDepth() >= 500)
        {
            isHooking = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fish"))
        {
            HandleFishCollision(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Trash"))
        {
            HandleTrashCollision(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Barrier"))
        {

        }
    }

    void HandleFishCollision(GameObject fish)
    {
        if (fishesOnHook.Count < maxFishCount && isHooking)
        {
            AddFishToHook(fish);
            // Increment the count of fishes collected.
            fishCount += 1;
            // Update the fish count display.
            SetFishCountText();
            // Audio for fish catch
            AudioManager.instance.PlaySoundClip(fishCatch, 50);

            // Check if the caught fish is green
            Fish fishScript = fish.GetComponent<Fish>();
            if (fishScript != null)
            {
                if (fishScript.Selected_Color == ColorOptions.Green)
                {
                    // Slow down other fishes for 5 seconds
                    StartCoroutine(SlowDownFishes(5f));
                }
                else if (fishScript.Selected_Color == ColorOptions.Yellow)
                {
                    // Freeze the hook for 2 seconds
					StartCoroutine(FreezeHookAndActivateLightning(4f, hook.position));

                }
                else if (fishScript.Selected_Color == ColorOptions.Red)
                {
                    // Speed up other fishes for 5 seconds
                    StartCoroutine(SpeedUpFishes(5f));
                }
            }
        }
        else
        {
            fish.SetActive(true);
        }
		

		foreach (GameObject i in fishesOnHook)
		{
			Fish fishScript = i.GetComponent<Fish>();
			ColorOptions colorOption = fishScript.Selected_Color;

			int fishIndex = fishesOnHook.IndexOf(i);
			if (fishIndex >= 0 && fishIndex < Fishrenderer.Length)
			{
				switch (colorOption)
				{
					case ColorOptions.Red:
						Fishrenderer[fishIndex].color = Color.red;
						break;
					case ColorOptions.Yellow:
						Fishrenderer[fishIndex].color = Color.yellow;
						break;
					case ColorOptions.Green:
						Fishrenderer[fishIndex].color = Color.green;
						break;
					case ColorOptions.Blue:
						Fishrenderer[fishIndex].color = Color.blue;
						break;
					case ColorOptions.Purple:
						Fishrenderer[fishIndex].color = Color.magenta;
						break;
					case ColorOptions.Black:
						Fishrenderer[fishIndex].color = Color.black;
						break;
					case ColorOptions.White:
						Fishrenderer[fishIndex].color = Color.white;
						break;
					default:
						Debug.LogError("Invalid color option: " + colorOption);
						break;
				}
			}
			else
			{
				Debug.LogError("Fish index out of range or Fishrenderer array is not large enough.");
			}
		}



	}
    void HandleTrashCollision(GameObject trash)
    {
        if (isHooking)
        {
			
			if (fishCount > 0)
			{
				Debug.Log("TRah");
				// Check if the fish has a SpriteRenderer component
				UnityEngine.U2D.SpriteShapeRenderer fishRenderer =  Fishrenderer[fishCount-1];
				// Set the alpha value of the fish to 0
				Color currentColor = fishRenderer.color;
				currentColor.a = 0f;
				fishRenderer.color = currentColor;
			
			// Reset fish count to zero
            fishCount--;
            // Update the fish count display
            SetFishCountText();
			

            LoseFish();
            Destroy(trash);
			}
            
        }
    }

    void AddFishToHook(GameObject fish)
    {
        fishesOnHook.Add(fish);
        fish.SetActive(false);

        fish.transform.position = fishHolder.position;
        fish.transform.SetParent(fishHolder);
    }

    void LoseFish()
    {
        fishesOnHook.RemoveAt(fishCount);
    }


    // Function to update the displayed count of fishes collected.
    void SetFishCountText()
    {
        fishCountText.text = "Fish Count: " + fishCount.ToString();
    }

    IEnumerator SlowDownFishes(float duration)
    {
        GameObject[] allFishes = GameObject.FindGameObjectsWithTag("Fish");

		// Increase the swim speed of all fishes
		foreach (GameObject fish in allFishes)
		{
			Fish fishScript = fish.GetComponent<Fish>();
			if (fishScript != null)
			{
				// Modify the swim speed for all fishes
				fishScript.swimSpeed = 3; // You can adjust the speed increase factor as needed
			}
		}

		// Wait for the specified duration
		yield return new WaitForSeconds(duration);

		// Restore the original swim speeds of fishes
		foreach (GameObject fish in originalFishSpeeds.Keys)
		{
			Fish fishScript = fish.GetComponent<Fish>();
			if (fishScript != null)
			{
				fishScript.swimSpeed = 7;
			}
		}
    }
    
	IEnumerator FreezeHookAndActivateLightning(float duration, Vector3 hookPosition)
	{
		// Freezing hook movement
        hookPlayerMovement.FreezeMovement();

        // Shortened delay before setting the position and activating the lightning
        yield return new WaitForSeconds(0.1f); // Adjusted

        // Setting the initial position of the lightning
        lightningEffect.transform.position = hookPosition;

        // Calculating flicker duration and frequency
        float flickerDuration = duration * 0.6f; // Adjusted to fit within 2 seconds
        float flickerFrequency = 0.05f; // Adjusted for smoother flickering

        // Activating the lightning effect and making it flicker
        float timer = 0f;
        while (timer < flickerDuration)
        {
            lightningEffect.SetActive(!lightningEffect.activeSelf); // Toggling the visibility of the lightning
            yield return new WaitForSeconds(flickerFrequency);
            timer += flickerFrequency;
        }

        // Deactivating the lightning effect
        lightningEffect.SetActive(false);

        // Waiting for the remaining duration after flickering
        yield return new WaitForSeconds(duration - flickerDuration);

        // Shortened delay before unfreezing
        yield return new WaitForSeconds(0.3f); // Adjusted

        // Unfreezing movement after the specified duration
        hookPlayerMovement.UnfreezeMovement();
	}


	IEnumerator SpeedUpFishes(float duration)
	{
		
		GameObject[] allFishes = GameObject.FindGameObjectsWithTag("Fish");

		// Increase the swim speed of all fishes
		foreach (GameObject fish in allFishes)
		{
			Fish fishScript = fish.GetComponent<Fish>();
			if (fishScript != null)
			{
				// Modify the swim speed for all fishes
				fishScript.swimSpeed = 10; // You can adjust the speed increase factor as needed
			}
		}

		// Wait for the specified duration
		yield return new WaitForSeconds(duration);

		// Restore the original swim speeds of fishes
		foreach (GameObject fish in originalFishSpeeds.Keys)
		{
			Fish fishScript = fish.GetComponent<Fish>();
			if (fishScript != null)
			{
				fishScript.swimSpeed = 7;
			}
		}
	}

}