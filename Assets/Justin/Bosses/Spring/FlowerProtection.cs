using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerProtection : MonoBehaviour
{
    public float rotateSpeed = 40f;
    public SpringCatfish catfish;
    public HealthComponent health;

    
    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            health.canBeDamaged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    
       
    
}
