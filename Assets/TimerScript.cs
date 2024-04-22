using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text object
    public string nextSceneName; // Name of the scene to load after the timer is up
    public bool started = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (gameObject.activeSelf == true && started == false)
        {
            started = true;
            Time.timeScale = 1f;
            StartCoroutine(StartTimer());
        }        
		if(totalTime==0){
			SceneManager.LoadScene(nextSceneName); 
		}
    }

    IEnumerator StartTimer()
    {
        float timeLeft = totalTime; // Initialize time left to total time

        while (timeLeft > 0)
        {
            int seconds = Mathf.FloorToInt(timeLeft);
            string timerString = $"{seconds}s"; // Format the timer text
            timerText.text = timerString; // Update the TextMeshPro text with the timer

            if (timeLeft <= 5f)
            {
                timerText.color = Color.red;
            }
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene("Main Menu");
			}
			
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timeLeft = timeLeft-1f; // Decrease time left by 1 second
            Debug.Log("Time left: " + timeLeft); // Log time left
        }

		SceneManager.LoadScene(nextSceneName);// Timer is up, load the next scene
    }
}
