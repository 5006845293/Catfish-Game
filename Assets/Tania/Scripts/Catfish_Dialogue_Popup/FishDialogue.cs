using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index = 0;

    public GameObject ContinueButton;
    public float wordSpeed;
    public bool playerIsClose;

    private bool isTyping = false;
    private bool dialogueCompleted = false;

    private Coroutine typingCoroutine;

    void Start()
    {
        dialogueText.text = "";
    }

    void Update()
    {
        if (playerIsClose && !isTyping && !dialogueCompleted)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                typingCoroutine = StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                if (index < dialogue.Length - 1)
                {
                    ContinueButton.SetActive(true);
                }
                else
                {
                    dialogueCompleted = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        StopCoroutine(typingCoroutine); // Stop the typing coroutine
        dialogueText.text = ""; // Clear dialogue text
        index = 0;
        dialoguePanel.SetActive(false);
        ContinueButton.SetActive(false);
        isTyping = false;
        dialogueCompleted = false;
    }

    IEnumerator Typing()
    {
        isTyping = true;
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (index < dialogue.Length - 1)
        {
            ContinueButton.SetActive(true);
        }
        else
        {
            dialogueCompleted = true;
        }

        isTyping = false;
    }

    public void NextLine()
    {
        ContinueButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingCoroutine = StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}
