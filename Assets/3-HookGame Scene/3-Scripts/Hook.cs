using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    public float movementSpeed = 5f;
    public Transform fishHolder;  
    public int maxFishCount = 5;
	public DepthController DepthScript;
    public bool isHooking = true;
    public List<GameObject> fishesOnHook = new List<GameObject>();
    private const float TopPositionY = 12.3f;
    private Vector2 hookOriginalPosition;
    [SerializeField] private AudioClip fishCatch;


    // Store original positions for fishes and trash when the game starts
    private Dictionary<GameObject, Vector2> originalFishPositions = new Dictionary<GameObject, Vector2>();
    private Vector2 originalTrashPosition;

    // Variable to keep track of collected fishesc.
    private int fishCount = 0;

    // UI text component to display count of fishes collected.
    public TextMeshProUGUI fishCountText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
		isHooking = true;
        // Store the original position of the hook
        hookOriginalPosition = rb.position;

        // Store the original positions of all fishes and trash
        StoreOriginalPositions();

        // Initialize fishCount to zero.
        fishCount = 0;

        // Update the fish count display.
        SetFishCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        if (Mathf.Abs(movementVector.y) > Mathf.Abs(movementVector.x))
        {
            horizontalMovement = 0f;
            rb.velocity = new Vector2(rb.velocity.x, movementVector.y * movementSpeed);
        }
        else
        {
            horizontalMovement = movementVector.x;
            rb.velocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
		if(DepthScript.GetCurrentDepth()>=500){
			isHooking = false;
		}
		
    }

    void HandleMovementInput()
    {
        Vector2 movement = new Vector2(horizontalMovement, 0.0f);
        rb.velocity = new Vector2(movement.x * movementSpeed, rb.velocity.y);
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
        }
        else
        {
            fish.SetActive(true);
        }
    }

    void HandleTrashCollision(GameObject trash)
	{
		if(isHooking){
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

        if (!originalFishPositions.ContainsKey(fish))
        {
            originalFishPositions.Add(fish, fish.transform.position);
        }

        fish.transform.position = fishHolder.position;
        fish.transform.SetParent(fishHolder);
    }

	void LoseFish()
	{
		
		fishesOnHook.RemoveAt(fishCount);
	}


    void ResetHookPosition()
    {
        Invoke("PerformResetHookPosition", 0.5f);
    }

    void PerformResetHookPosition()
    {
        rb.position = hookOriginalPosition;
        rb.velocity = Vector2.zero;

        // Reset fish count to zero
        fishCount = 0;

        // Update the fish count display
        SetFishCountText();

        ActivateAllFishes();
    }

    void ActivateAllFishes()
    {
        foreach (GameObject fish in originalFishPositions.Keys)
        {
            if (originalFishPositions.TryGetValue(fish, out Vector2 originalPosition))
            {
                fish.SetActive(true);
                fish.transform.position = originalPosition;
                fish.transform.SetParent(null);
            }
        }

        fishesOnHook.Clear();
    }

    void StoreOriginalPositions()
    {
        GameObject[] allFishes = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject fish in allFishes)
        {
            if (!originalFishPositions.ContainsKey(fish))
            {
                originalFishPositions.Add(fish, fish.transform.position);
            }
        }

        GameObject trash = GameObject.FindGameObjectWithTag("Trash");
        if (trash != null)
        {
            originalTrashPosition = trash.transform.position;
        }
    }

    // Function to update the displayed count of fishes collected.
    void SetFishCountText()
    {
        fishCountText.text = "Fish Count: " + fishCount.ToString();
    }
	

}