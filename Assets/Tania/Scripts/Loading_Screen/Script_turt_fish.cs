using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Script_turt_fish : MonoBehaviour
{
    public Script_spawn spawner;
    public GameObject game_area;
 
    public float speed;
 
    void Update()
    {
        Move();
    }
 
    void Move()
    {
 
        transform.position += transform.up * (Time.deltaTime * speed);
 
        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if(distance > spawner.death_circle_radius)
        {
            Remove();
        }
    }
 
    void Remove()
    {
 
        Destroy(gameObject);
        spawner.count -= 1;
    }
}