using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BacktoGallery : MonoBehaviour
{
    void Start()
    {
        // Find the button component attached to this GameObject
        Button button = GetComponent<Button>();

        if (button)
        {
            // Add a listener to the button's onClick event, specifying the LoadScene method to be called when clicked
            button.onClick.AddListener(LoadScene);
        }
    }

    // This method will be called when the button is clicked
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
 
    }
    
}
