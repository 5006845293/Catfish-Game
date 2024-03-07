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

    void Start()
    {
        dialogueText.text = "";
    }

    void Update()
    {
        if (playerIsClose && !isTyping)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                if (index < dialogue.Length - 1)
                {
                    ContinueButton.SetActive(true);
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
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        ContinueButton.SetActive(false);
        isTyping = false;
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
            RemoveText();
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
            StartCoroutine(Typing());
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