using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TextBoxDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] Image textBackdrop;
    [SerializeField] Image textButton;
    [SerializeField] InputAction skipButton;

    PlayerController playerController;

    bool showText;

    public string[] texts;
    string displayText;

    int currentIndex;

    bool skipText;
    bool textDisplayFinished;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        showText = texts != null;

        displayText = "empty";
        textDisplayFinished = true;

        currentIndex = -1;

        TryDisplayNextText();
    }

    private void OnEnable()
    {
        // re init the textbox
        currentIndex = 0;
        showText = texts != null;
        TryDisplayNextText();

        skipButton.performed += ctx => TryDisplayNextText();
        skipButton.Enable();

        playerController.DisableControls();
    }

    private void OnDisable()
    {
        skipButton.Disable();

        playerController.EnableControls();
    }

    private void Update()
    {
        if (showText)
        {
            skipText = skipButton.IsPressed(); // holding down the key should speed up text

            if (textDisplayFinished)
                textButton.color = IsOddTime(.75f) ? Color.white : Color.clear;
            else
                textButton.color = Color.clear;

            textBox.text = displayText;
        }
    }

    void TryDisplayNextText()
    {
        if (textDisplayFinished)
        {
            currentIndex++;
            textDisplayFinished = false;

            if (currentIndex < texts.Length)
                StartCoroutine(DisplayText());
            else
                CloseTextbox();
        }
    }

    IEnumerator DisplayText()
    {
        char[] characters = texts[currentIndex].ToCharArray();
        int characterIndex = 0;
        displayText = "";

        while (characterIndex < texts[currentIndex].Length)
        {
            displayText += characters[characterIndex];

            characterIndex++;

            yield return new WaitForSeconds(skipText ? .001f : .03f);
        }

        textDisplayFinished = true;
    }

    public void CloseTextbox()
    {
        textBox.CrossFadeAlpha(0f, .25f, false);
        textBackdrop.CrossFadeAlpha(0f, .25f, false);
        textButton.color = Color.clear;
        enabled = false;

        showText = false;
    }

    // alternates between true and false based on the time interval
    static bool IsOddTime(float interval)
    {
        return (Time.realtimeSinceStartup % interval) / interval < .5f;
    }
}
