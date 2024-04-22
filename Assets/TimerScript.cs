using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text object
    public string nextSceneName; // Name of the scene to load after the timer is up

    void Start()
    {
        StartCoroutine(StartTimer());
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

            yield return new WaitForSeconds(1f); // Wait for 1 second
            timeLeft -= 1f; // Decrease time left by 1 second
        }

        SceneManager.LoadScene(nextSceneName); // Timer is up, load the next scene
    }
}
