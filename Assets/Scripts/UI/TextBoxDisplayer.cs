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
    [SerializeField] EndScreenDisplayer endScreenScript;

    PlayerController playerController;

    bool showText;

    static string[][] sequenceList =
    {
        new string[]
        {
            "Welcome, weary warrior. Do you remember where you are?",
            "...Not a worry, you will soon remember.",
            "Gaze around you, for this land you find yourself within is in the heat of battle.",
            "Thus, I present to you a task: there is a camp of legionaires nearby, you must take them down.",
            "Complete this task, and return here. More will await you.",
            "...And remember, you may pause and review your controls by pressing [TAB] or [START].",
            "DEFEAT 8 LEGIONAIRES"
        },
        new string[]
        {
            "A job well done indeed. Are you beginning to understand who you are?",
            "...",
            "...No matter, continue along your quests and it will surely occur to you.",
            "Now that you've completed a quest, return to the camp you found yourself in and declare victory."
        },
        new string[]
        {
            "Quite the feeling, no?",
            "The path is now cleared; proceed through the route just outside to reach Thermopylae fields.",
            "Further conquest awaits you there."
        },
        new string[]
        {
            "You have arrived. You understand the drill.",
            "Strike down your enemies, and this area will soon be conquered.",
            "Be wary of getting ahead of yourself now; your adversaries grow in strength.",
            "DEFEAT 5 HIGH LEGIONARIES"
        },
        new string[]
        {
            "Excellent, excellent... don't you feel that rush returning to you?",
            "THIS IS YOUR PURPOSE, WARRIOR!",
            "Return once again, declare your victory, and prepare to take down the rest."
        },
        new string[]
        {
            "Are you ready, Warrior?",
            "If your history has not occured to you yet, I am certain it will after this encounter.",
            "Go forth. Claim this camp as your own.",
            "DEFEAT 3 CAMP GUARDS"
        },
        new string[]
        {
            "Well done, truly.",
            "*Suddenly, a feeling of both dread and relief runs through the warrior.*",
            "So... you recognize these people for who they truly are, don't you?",
            "The people...",
            "WHO SLAUGHTERED YOU!",
            "*The feeling grows stronger, until a collapse.*"
        }
    };
    static DialogueType[] sequenceType =
    {
        DialogueType.QUEST, DialogueType.AUTO, DialogueType.MANUAL, DialogueType.QUEST, DialogueType.AUTO, DialogueType.QUEST, DialogueType.AUTO
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

            if (characterIndex % 2 == 0)
                yield return new WaitForSeconds(.001f);
        }

        if (currentIndex == texts.Length - 1)
            switch (sequenceType[sequenceIndex - 1])
            {
                case DialogueType.QUEST: 
                    selectWindow.SetActive(true); 
                    PlayerController.textPermitted = false; 
                    break;
                case DialogueType.AUTO:
                    if (sequenceIndex == sequenceList.Length)
                        endScreenScript.BeginEndScreen();
                    PlayerController.textPermitted = true; 
                    break;
            }

        textDisplayFinished = true;
    }

    // alternates between true and false based on the time interval
    static bool IsOddTime(float interval)
    {
        return (Time.realtimeSinceStartup % interval) / interval < .5f;
    }

    enum DialogueType
    {
        QUEST,
        AUTO,
        MANUAL
    }
}
