using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
	public GameObject popUpMenu;
	public GameObject GalleryMenu;
	public static bool popUpActive;
	
    // Start is called before the first frame update
    void Start()
    {
		popUpMenu.SetActive(true);
		GalleryMenu.SetActive(true);
		popUpActive = true;
		Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Gallery"){
			if(popUpActive){
				Resume();
			}
			else{
				PopUpOpen();
			}
		}
    }
	
	void Resume(){
		popUpActive = false;
		popUpMenu.SetActive(false);
		GalleryMenu.SetActive(false);
		Time.timeScale = 1f;
	}
	
	void PopUpOpen(){
		popUpActive = true;
		popUpMenu.SetActive(true);
		GalleryMenu.SetActive(true);
		Time.timeScale = 0f;
	}
}