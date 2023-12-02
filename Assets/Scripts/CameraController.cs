using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 public GameObject hook;

 private Vector3 offset;


 void Start()
    {
        offset = transform.position - hook.transform.position; 
    }


 void LateUpdate()
    {

        transform.position = hook.transform.position + offset;  
    }
}