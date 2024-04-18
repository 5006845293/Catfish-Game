using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Fadeinout : MonoBehaviour
{
	public Image image;
	public Image dialogBack;
    public TextMeshProUGUI dialogText;
    public float fadeDuration = 1.0f;
    public Sprite[] images;
    public string[] dialogues;
    private int currentIndex = 0;
    private bool isFading = false;

    void Start()
    {
        ShowImageAndText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isFading)
        {
            NextImageAndText();
        }
    }

    void ShowImageAndText()
    {
        image.sprite = images[currentIndex];
        dialogText.text = dialogues[currentIndex];
        FadeIn();
    }

    void NextImageAndText()
    {
        FadeOut();
        currentIndex++;
        if (currentIndex < images.Length)
        {
            Invoke("ShowImageAndText", fadeDuration);
        }
        else
        {
            // All images and dialogues shown, transition to gallery scene
            SceneManager.LoadScene("Gallery");
        }
    }

    void FadeIn()
    {
        isFading = true;
        StartCoroutine(FadeToAlpha(image, 1.0f, fadeDuration));
        StartCoroutine(FadeToAlpha(dialogText, 1.0f, fadeDuration));
		StartCoroutine(FadeToAlpha(dialogBack, 1.0f, fadeDuration));
    }

    void FadeOut()
    {
        isFading = true;
        StartCoroutine(FadeToAlpha(image, 0.0f, fadeDuration));
        StartCoroutine(FadeToAlpha(dialogText, 0.0f, fadeDuration));
		StartCoroutine(FadeToAlpha(dialogBack, 0.0f, fadeDuration));
    }

    IEnumerator FadeToAlpha(Graphic graphic, float targetAlpha, float duration)
    {
        Color startColor = graphic.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            graphic.color = Color.Lerp(startColor, targetColor, progress);
            yield return null;
        }

        graphic.color = targetColor;
        isFading = false;
    }
}
