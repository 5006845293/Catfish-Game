using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float floatSpeed = 0.5f; // Speed of the fish's floating movement
    public float floatAmplitude = 0.1f; // Amplitude of the floating motion

    void Update()
    {
        FloatInPlace();
    }

    void FloatInPlace()
    {
        // Move the fish vertically (up and down) with reduced amplitude
        Vector2 movement = new Vector2(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude);

        // Move the fish
        transform.Translate(movement * floatSpeed * Time.deltaTime);
    }
}
