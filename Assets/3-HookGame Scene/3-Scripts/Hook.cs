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
    public UnityEngine.U2D.SpriteShapeRenderer[] Fishrenderer;
    public GameObject lightningEffect;
    public Transform hook; 

    // Variable to keep track of collected fishes.
    private int fishCount = 0;

    // UI text component to display count of fishes collected.
    public TextMeshProUGUI fishCountText;

    void Start()
    {
        // Getting Rigidbody2D component and setting constraints
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        isHooking = true;

        // Deactivating the lightning effect if it's not null
        if (lightningEffect != null)
        {
            lightningEffect.SetActive(false);
        }

        // Initializing fishCount to zero.
        fishCount = 0;

        // Updating the fish count display.
        SetFishCountText();
    }

    private void FixedUpdate()
    {
        // Checking if the depth is greater than or equal to 500 to stop hooking
        if (DepthScript.GetCurrentDepth() >= 500)
        {
            isHooking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Handling collisions based on tags
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
            // Additional actions for collision with barrier
        }
    }

    void HandleFishCollision(GameObject fish)
    {
        // Checking if the hook can hold more fishes and still hooking
        if (fishesOnHook.Count < maxFishCount && isHooking)
        {
            // Adding fish to the hook
            AddFishToHook(fish);
            // Incrementing the count of fishes collected.
            fishCount += 1;
            // Updating the fish count display.
            SetFishCountText();
            // Playing audio for fish catch
            AudioManager.instance.PlaySoundClip(fishCatch, 50);

            // Check if the caught fish is green
            Fish fishScript = fish.GetComponent<Fish>();
            if (fishScript != null)
            {
                if (fishScript.Selected_Color == ColorOptions.Green)
                {
                    // Slowing down other fishes for 5 seconds
                    StartCoroutine(SlowDownFishes(5f));
                }
                else if (fishScript.Selected_Color == ColorOptions.Yellow)
                {
                    // Freezing hook and activating lightning for 4 seconds
                    StartCoroutine(FreezeHookAndActivateLightning(4f, hook.position));
                }
                else if (fishScript.Selected_Color == ColorOptions.Red)
                {
                    // Speeding up other fishes for 5 seconds
                    StartCoroutine(SpeedUpFishes(5f));
                }
            }
        }
        else
        {
            // Enabling fish if hook cannot hold more
            fish.SetActive(true);
        }

        // Coloring fishes based on their selected color
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
        // Checking if still hooking
        if (isHooking)
        {
            Debug.Log("Hit trash, losing 1 fish!");
            // Decreasing the fish count by 1
            fishCount = Mathf.Max(0, fishCount - 1);
            // Updating the fish count display
            SetFishCountText();

            // Removing the last fish from the hook
            if (fishesOnHook.Count > 0)
            {
                GameObject lastFish = fishesOnHook[fishesOnHook.Count - 1];
                fishesOnHook.Remove(lastFish);
                Destroy(lastFish);
            }

            // Destroying the trash object
            Destroy(trash);
        }
    }

    void AddFishToHook(GameObject fish)
    {
        // Adding fish to the hook
        fishesOnHook.Add(fish);
        fish.SetActive(false);

        // Positioning fish and setting parent
        fish.transform.position = fishHolder.position;
        fish.transform.SetParent(fishHolder);
    }

    void LoseFish()
    {
        if (fishesOnHook.Count > 0)
        {
            GameObject lastFish = fishesOnHook[fishesOnHook.Count - 1];
            fishesOnHook.Remove(lastFish);
            Destroy(lastFish);
        }
        else
        {
            Debug.LogWarning("Tried to lose fish, but there are no fishes on the hook.");
        }
    }


    // Function to update the displayed count of fishes collected.
    void SetFishCountText()
    {
        fishCountText.text = "Fish Count: " + fishCount.ToString();
    }

    IEnumerator SlowDownFishes(float duration)
    {
        GameObject[] allFishes = GameObject.FindGameObjectsWithTag("Fish");

        // Slowing down all fishes
        foreach (GameObject fish in allFishes)
        {
            Fish fishScript = fish.GetComponent<Fish>();
            if (fishScript != null)
            {
                // Modifying the swim speed for all fishes
                fishScript.swimSpeed = 3; // You can adjust the speed increase factor as needed
            }
        }

        // Waiting for the specified duration
        yield return new WaitForSeconds(duration);

        // Restoring the original swim speeds of fishes
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

        // Speeding up all fishes
        foreach (GameObject fish in allFishes)
        {
            Fish fishScript = fish.GetComponent<Fish>();
            if (fishScript != null)
            {
                // Modifying the swim speed for all fishes
                fishScript.swimSpeed = 10; // You can adjust the speed increase factor as needed
            }
        }

        // Waiting for the specified duration
        yield return new WaitForSeconds(duration);

        // Restoring the original swim speeds of fishes
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
