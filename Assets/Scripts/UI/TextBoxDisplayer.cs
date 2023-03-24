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

    [SerializeField] GameObject selectWindow;

    PlayerController playerController;

    bool showText;

    static string[][] sequenceList =
    {
        new string[]
        {
            "Welcome, weary warrior. Do you remember where you are?",
            "...Not a worry, you will soon remember.",
            "For now, I present to you a task: there is a camp of legionaires nearby, you must take them down.",
            "Complete this task, and return here. More will await you.",
            "...And remember, you can move using the [WASD] keys, and perform attacks using [LEFT MOUSE] and [RIGHT MOUSE].",
            "DEFEAT 3 LEGIONAIRES"
        },
        new string[]
        {
            "A job well done indeed. Are you beginning to understand who you are?",
            "...",
            "...No matter, simply continue along your quests and it shall occur to you.",
            "The path is now cleared; proceed through the path just outside to reach Thermopylae fields."
        },
        new string[]
        {
            "You have arrived. You know the drill.",
            "Strike down your enemies, and this area will have been conquered.",
            "DEFEAT 5 LEGIONARIES"
        }
    };
    static int sequenceIndex;

    string[] texts;
    string displayText;

    int currentIndex;

    bool textDisplayFinished;

    static bool playedFirstSequence;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        selectWindow.SetActive(false);

        displayText = "empty";

        if (!playedFirstSequence)
        {
            BeginNewDialogue();
            playedFirstSequence = true;
        }
        else
        {
            CloseTextbox();
        }

        skipButton.performed += ctx => TryDisplayNextText();
    }

    public void BeginNewDialogue()
    {
        // re init the textbox
        currentIndex = -1;
        textDisplayFinished = true;

        texts = sequenceList[sequenceIndex++]; // set the displaying text sequence to the current one on the list, and increase the index for next time.

        OpenTextbox();
        TryDisplayNextText();


    }

    void OpenTextbox()
    {
        textBox.CrossFadeAlpha(1f, .1f, false);
        textBackdrop.CrossFadeAlpha(.75f, .1f, false);
        textButton.color = Color.white;

        showText = texts != null;
        skipButton.Enable();

        playerController.DisableControlsWithCursorUnlock();
    }

    public void CloseTextbox()
    {
        selectWindow.SetActive(false);

        textBox.CrossFadeAlpha(0f, .25f, false);
        textBackdrop.CrossFadeAlpha(0f, .25f, false);
        textButton.color = Color.clear;

        showText = false;
        skipButton.Disable();

        playerController.EnableControls();
    }

    private void Update()
    {
        if (showText)
        {
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

            if (characterIndex % 3 == 0)
                yield return new WaitForSeconds(.001f);
        }

        if (currentIndex == texts.Length - 1 && sequenceIndex != 2)
            selectWindow.SetActive(true);

        textDisplayFinished = true;
    }

    // alternates between true and false based on the time interval
    static bool IsOddTime(float interval)
    {
        return (Time.realtimeSinceStartup % interval) / interval < .5f;
    }
}
