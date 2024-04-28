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

    // Variable to keep track of collected fishes.
    private int fishCount = 0;

    // UI text component to display count of fishes collected.
    public TextMeshProUGUI fishCountText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
                    StartCoroutine(FreezeHook(4f));
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
    }

    void HandleTrashCollision(GameObject trash)
    {
        if (isHooking)
        {
            Debug.Log("Hit trash, losing 1 fish!");
            // Reset fish count to zero
            fishCount--;
            // Update the fish count display
            SetFishCountText();

            LoseFish();
            Destroy(trash);
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
				fishScript.swimSpeed = 4; // You can adjust the speed increase factor as needed
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

    IEnumerator FreezeHook(float duration)
    {
        // Disable movement for the hook
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(duration);

        // Enable movement for the hook after the specified duration
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
