using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check tag of object colliding
        if (collision.gameObject.CompareTag("Barrier"))
        {
            // Prevent the player from moving through the barrier
            StopPlayerMovement();

            Debug.Log("Player collided with an obstacle!");

        }
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

    }

    void StopPlayerMovement()
    {
        // stop the player's Rigidbody velocity
        rb.velocity = new Vector2(0, 0);
    }
    


}


