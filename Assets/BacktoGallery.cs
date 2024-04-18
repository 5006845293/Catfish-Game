using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BacktoGallery : MonoBehaviour
{
    void Start()
    {
    }
	
	void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Gallery");
        }
    }

    
}
