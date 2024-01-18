using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorOptions
{
	Red,
	Yellow,
	Green,
	Blue,
	Purple,
	White,
	Black
}
public enum DirectionOptions
{
	Right,
	Left
}

	
public class Fish : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Texture2D texture;
	
	public ColorOptions Selected_Color;
	public DirectionOptions Selected_Direction;
	public float swimSpeed = 7.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        string fileName = "BaitFish_" + Selected_Color.ToString() + ".png"; // Folder name corresponds to enum option
        string filePath = "Assets/3-HookGame Scene/3-Textures/Fish/BaitFish/" + fileName;
		texture = LoadTextureFromFile(filePath);
		if (texture != null){
            // Create a sprite from the loaded texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Set the sprite to the Sprite Renderer
            spriteRenderer.sprite = sprite;
        }
		
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
	
	
	private Texture2D LoadTextureFromFile(string path){
		
        Texture2D tex = new Texture2D(2, 2);
        byte[] fileData = System.IO.File.ReadAllBytes(path);

        if (fileData != null){
            tex.LoadImage(fileData);
            return tex;
        }
        else{
            Debug.LogError("Failed to read file data at path: " + path);
            return null;
        }
    }
}
