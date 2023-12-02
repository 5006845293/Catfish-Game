using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthCounter : MonoBehaviour
{
    
    [SerializeField] private TMP_Text depth;
    private int currentDepth = 0;
    private float lastPlayerYPosition;
    private float updateCooldown = 1f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        lastPlayerYPosition = transform.position.y;
    }
    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0f) 
        {
            float currentYPosition = transform.position.y;

            if (Input.GetAxis("Vertical") < 1f)
            {
                float depthChange = lastPlayerYPosition - currentYPosition;
                int depthChangeInt = Mathf.FloorToInt(depthChange);
                increaseDepth(depthChangeInt);
                cooldownTimer = updateCooldown;
            }

            lastPlayerYPosition = currentYPosition;
        }
        
    }





    public void increaseDepth(int amount)
    {
        currentDepth += amount;
        depth.text = "Depth: " + currentDepth;

    }


}
