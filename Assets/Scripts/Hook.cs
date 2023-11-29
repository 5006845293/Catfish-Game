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
            // Prioritize vertical movement over horizontal
            horizontalMovement = 0f;
            // You can also adjust this to clamp vertical movement to either 1 or -1 if needed
            rb.velocity = new Vector2(rb.velocity.x, movementVector.y * movementSpeed);
        }
        else
        {
            // Prioritize horizontal movement over vertical
            horizontalMovement = movementVector.x;
            rb.velocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        }
    }


    void FixedUpdate()
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

        Debug.Log("Colliding, isHooking set to true");

        if (collider.CompareTag("Fish"))
        {   
            HandleFishCollision(collider.gameObject);
            Debug.Log("Collided with Fish");
        }
        else if (collider.CompareTag("Trash"))
        {
            HandleTrashCollision(collider.gameObject);
            Debug.Log("Collided with Trash");
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
            Destroy(fish);
        }
    }

    void HandleTrashCollision(GameObject trash)
    {
        Debug.Log("Hit trash, losing all fish!");
        LoseAllFish();

        Destroy(trash); // Destroy the collided trash
    }

    void AddFishToHook(GameObject fish)
    {
        if (fishesOnHook.Count < maxFishCount)
        {
            fishesOnHook.Add(fish);

            // Disable the fish by setting it inactive
            fish.SetActive(false);

            // Set the position of the fish relative to the fish holder
            fish.transform.position = fishHolder.position;

            // Set the fish as a child of the fish holder
            fish.transform.SetParent(fishHolder);
        }
        else
        {
            Destroy(fish);
        }
    }

    void LoseAllFish()
    {
        foreach (GameObject fish in fishesOnHook)
        {
            // Enable the fish by setting it active
            fish.SetActive(true);

            // Set the fish's parent back to null (no longer on the hook)
            fish.transform.SetParent(null);
        }

        fishesOnHook.Clear();
    }
}
