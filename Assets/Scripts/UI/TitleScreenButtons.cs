using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    [SerializeField] GameObject creditsObj;

    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
    public void ButtonCredits()
    {
        creditsObj.SetActive(true);
    }
}
