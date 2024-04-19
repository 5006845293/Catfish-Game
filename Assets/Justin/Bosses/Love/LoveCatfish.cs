using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveCatfish : MonoBehaviour
{

    public GameObject player;
    public float avoidDist;
    public float speed;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) { return; }

        
    }
    
    private void Follow()
    {
        Vector3 rayDirection = player.transform.position - transform.position;
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, rayDirection, avoidDist, LayerMask.GetMask("Player"));

        if(!lineOfSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

}
