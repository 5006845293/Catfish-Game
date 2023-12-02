using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    public float movementSpeed = 5f;
    public Transform fishHolder;
    public int maxFishCount = 5;
    private bool isHooking = false;
    private List<GameObject> fishesOnHook = new List<GameObject>();
    private const float TopPositionY = 12.3f;

    public Transform green_grass_left; // Reference to the left grass wall transform
    public Transform green_grass_right; // Reference to the right grass wall transform

    private const string FishPositionKey = "FishPosition";
    private const string TrashPositionKey = "TrashPosition";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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

        if (isHooking)
        {
            LowerHook();
        }
    }

    void HandleMovementInput()
    {
        Vector2 movement = new Vector2(horizontalMovement, 0.0f);
        rb.velocity = new Vector2(movement.x * movementSpeed, rb.velocity.y);
    }

    void LowerHook()
    {
        Vector2 hookMovement = new Vector2(0f, -1f) * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + hookMovement);
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
        else if (collider.gameObject.CompareTag("BottomGrass"))
        {
            SavePositions();
            ResetHookPosition();
        }
    }

    void HandleFishCollision(GameObject fish)
    {
        if (fishesOnHook.Count < maxFishCount)
        {
            AddFishToHook(fish);
        }
        else
        {
            fish.SetActive(true);
        }
    }

    void HandleTrashCollision(GameObject trash)
    {
        Debug.Log("Hit trash, losing all fish!");
        LoseAllFish();
        Destroy(trash);
    }

    void AddFishToHook(GameObject fish)
    {
        fishesOnHook.Add(fish);
        fish.SetActive(false);
        fish.transform.position = fishHolder.position;
        fish.transform.SetParent(fishHolder);
    }

    void LoseAllFish()
    {
        foreach (GameObject fish in fishesOnHook)
        {
            fish.SetActive(false);
            fish.transform.SetParent(null);
        }

        fishesOnHook.Clear();
    }

    void SavePositions()
    {
        SaveFishPositions();
        SaveTrashPositions();
    }

    void SaveFishPositions()
    {
        for (int i = 0; i < fishesOnHook.Count; i++)
        {
            PlayerPrefs.SetFloat($"{FishPositionKey}_{i}_X", fishesOnHook[i].transform.position.x);
            PlayerPrefs.SetFloat($"{FishPositionKey}_{i}_Y", fishesOnHook[i].transform.position.y);
            PlayerPrefs.SetString($"{FishPositionKey}_{i}_Tag", fishesOnHook[i].tag);
        }
    }

    void SaveTrashPositions()
    {
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        for (int i = 0; i < trashObjects.Length; i++)
        {
            PlayerPrefs.SetFloat($"{TrashPositionKey}_{i}_X", trashObjects[i].transform.position.x);
            PlayerPrefs.SetFloat($"{TrashPositionKey}_{i}_Y", trashObjects[i].transform.position.y);
        }
    }

    void ResetHookPosition()
    {
        rb.position = new Vector2(rb.position.x, TopPositionY);
        rb.velocity = Vector2.zero;
        LoadSavedPositions();
        PositionFishesAndTrashBetweenGrassWalls(green_grass_left, green_grass_right);
    }

    void LoadSavedPositions()
    {
        LoadFishPositions();
        LoadTrashPositions();
    }

    void LoadFishPositions()
    {
        for (int i = 0; i < fishesOnHook.Count; i++)
        {
            float xPos = PlayerPrefs.GetFloat($"{FishPositionKey}_{i}_X", 0f);
            float yPos = PlayerPrefs.GetFloat($"{FishPositionKey}_{i}_Y", TopPositionY);
            string fishTag = PlayerPrefs.GetString($"{FishPositionKey}_{i}_Tag", "Fish");
            
            fishesOnHook[i].transform.position = new Vector3(xPos, yPos, 0f);
            fishesOnHook[i].tag = fishTag;
        }
    }

    void LoadTrashPositions()
    {
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        for (int i = 0; i < trashObjects.Length; i++)
        {
            float xPos = PlayerPrefs.GetFloat($"{TrashPositionKey}_{i}_X", 0f);
            float yPos = PlayerPrefs.GetFloat($"{TrashPositionKey}_{i}_Y", TopPositionY);
            trashObjects[i].transform.position = new Vector3(xPos, yPos, 0f);
        }
    }

    void PositionFishesAndTrashBetweenGrassWalls(Transform green_grass_left, Transform green_grass_right)
    {
        float xPos = (green_grass_left.position.x + green_grass_right.position.x) / 2f;

        for (int i = 0; i < fishesOnHook.Count; i++)
        {
            float fishOffsetX = i * 1.0f;
            Vector3 fishNewPosition = new Vector3(xPos + fishOffsetX, fishesOnHook[i].transform.position.y, 0f);
            fishesOnHook[i].transform.position = fishNewPosition;
        }

        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        for (int i = 0; i < trashObjects.Length; i++)
        {
            float trashOffsetX = i * 0.5f;
            Vector3 trashNewPosition = new Vector3(xPos + trashOffsetX, trashObjects[i].transform.position.y, 0f);
            trashObjects[i].transform.position = trashNewPosition;
        }
    }
}
