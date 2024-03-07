using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform[] zoomTargets;
    public GameObject[] popUps;
    public string[] dialogues;
    public float zoomSpeed = 2f;
    public float dialogueDisplayTime = 3f;

    private Text dialogueText;
    private int currentTargetIndex = 0;
    private bool isPaused = true;

    void Start()
    {
        dialogueText = GameObject.FindWithTag("DialogueText").GetComponent<Text>(); // Assuming you have a UI Text element tagged as "DialogueText"
        PauseGame(); // Pause the game at the beginning
        StartCoroutine(StartTutorial());
    }

    void Update()
    {
        // Check for input to move to the next object
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToNextObject();
        }
    }

    IEnumerator StartTutorial()
    {
        foreach (Transform target in zoomTargets)
        {
            yield return StartCoroutine(ZoomIn(target.position));
            yield return new WaitForSeconds(dialogueDisplayTime);
            ShowPopup(currentTargetIndex);
            dialogueText.text = dialogues[currentTargetIndex];
            yield return new WaitForSeconds(dialogueDisplayTime);
            HidePopup(currentTargetIndex);
            dialogueText.text = "";
            yield return StartCoroutine(ZoomOut());
            currentTargetIndex++;
        }

        ResumeGame(); // Resume the game after the tutorial completes
    }

    void MoveToNextObject()
    {
        if (!isPaused && currentTargetIndex < zoomTargets.Length - 1)
        {
            currentTargetIndex++;
            StartCoroutine(ZoomIn(zoomTargets[currentTargetIndex].position));
        }
    }

    IEnumerator ZoomIn(Vector3 targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.05f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            yield return null;
        }
    }

    IEnumerator ZoomOut()
    {
        Vector3 originalPosition = mainCamera.transform.position;
        while (Vector3.Distance(mainCamera.transform.position, originalPosition) > 0.05f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
            yield return null;
        }
    }

    void ShowPopup(int index)
    {
        popUps[index].SetActive(true); // Activate the popup GameObject corresponding to the current index
    }

    void HidePopup(int index)
    {
        popUps[index].SetActive(false); // Deactivate the popup GameObject corresponding to the current index
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game by setting time scale to 1
    }
}
