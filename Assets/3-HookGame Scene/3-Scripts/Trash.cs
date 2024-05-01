using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOptions
{
	Bag
}


public class Trash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
	
	public TypeOptions Selected_Type;
	public DirectionOptions Selected_Direction;
	public float swimSpeed = 7.0f;
	
    // Start is called before the first frame update
    void Start()
    {

		
    }

    // Update is called once per frame
    void Update()
    {
        // Move fish in the selected direction
        if (Selected_Direction == DirectionOptions.Right)
        {
            transform.Translate(Vector3.right * swimSpeed * Time.deltaTime);
			spriteRenderer.flipX = true;
        }
        else if (Selected_Direction == DirectionOptions.Left)
        {
            transform.Translate(Vector3.left * swimSpeed * Time.deltaTime);
			spriteRenderer.flipX = false;
        }
    }
	

}
