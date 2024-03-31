using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
	public GameObject popUpMenu;
	public static bool popUpActive;
	int count = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		popUpMenu.SetActive(true);
		popUpActive = true;
		Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
		//This makes sure the pop-up can not be opened over and over
        if(Input.GetKeyDown(KeyCode.Escape) && count == 0){
			if(popUpActive){
				Resume();
				count += 1;
			}
		}
    }
	
	void Resume(){
		popUpActive = false;
		popUpMenu.SetActive(false);
		Time.timeScale = 1f;
	}
}
