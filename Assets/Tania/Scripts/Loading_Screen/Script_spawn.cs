using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_spawn : MonoBehaviour
{
    public GameObject game_area; 
    public GameObject parent_prefab;

    public int count = 0;
    public int limit = 150;
    public int per_frame = 1;

    public float spawn_circle_radius = 80.0f;
    public float death_circle_radius = 90.0f;

    public float fastest_speed = 20.0f;
    public float slowest_speed = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        MaintainPopulation();
    }

    void InitialPopulation()
    {
 
        for(int i=0; i<limit; i++)
        {
            Vector3 position = GetRandomPosition(true);
            Script_turt_fish Script_fish = AddObject(position);
            Script_fish.transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
        }
    }

    void MaintainPopulation(){

        if (count < limit){
            for (int i=0; i<per_frame; i++){
                Vector3 position = GetRandomPosition(false);
                Script_turt_fish Script_fish = AddObject(position);
                Script_fish.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f));
            }
        }
    }

    Vector3 GetRandomPosition(bool within_camera){
        Vector3 position = Random.insideUnitCircle;

        if(within_camera == false)
        {
            position = position.normalized;
        }

        position *= spawn_circle_radius;
        position += game_area.transform.position;
        
        return position;

    }

    Script_turt_fish AddObject(Vector3 position){
        count += 1;
        GameObject new_object = Instantiate(
            parent_prefab, 
            position,
            Quaternion.FromToRotation(Vector3.up, (game_area.transform.position-position)),
            gameObject.transform
        );

        Script_turt_fish Script_fish = new_object.GetComponent<Script_turt_fish>();

        Script_fish.spawner = this;
        Script_fish.game_area = game_area;
        Script_fish.speed = Random.Range(slowest_speed, fastest_speed);
 
        return Script_fish;

        }
}