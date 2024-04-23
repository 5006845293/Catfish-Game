using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class FireCatfish : MonoBehaviour
{
    public float startValue = -6f;
    public float endValue = 6f;
    public float duration = 1f;
    public Transform flamePoint;

    private float timer;
    [SerializeField] private float rayDist = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        Vector3 point = transform.position;
        Vector3 axis = new Vector3(0, 0, 1);
        
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / duration, 1f);
        float interpolateVal = Mathf.Lerp(startValue, endValue, t);

        ;
        transform.RotateAround(point, axis, 0.3f);
        transform.position = new Vector3(interpolateVal, 0, interpolateVal);

        Flamethrower();
        
    }

    private void Flamethrower()
    {
        RaycastHit2D flameStart = Physics2D.Raycast(flamePoint.position, Vector2.left, rayDist, LayerMask.GetMask("Player"));

        if(flameStart.collider != null)
        {
            if(flameStart.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position, Vector2.left, Color.red, 0);
                Debug.Log("Player Hit!");
                hc = GetComponent<HealthComponent>();
            }
        }
        
        
    }
        
        
}
        


