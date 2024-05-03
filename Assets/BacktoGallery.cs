using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BacktoGallery : MonoBehaviour
{
	public GameObject menu;
    void Start()
    {
    }
	
	void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space)&& menu.activeSelf)
        {
            SceneManager.LoadScene("Gallery");
        }
    }

    
}
