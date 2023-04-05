using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenuDisplayer : MonoBehaviour
{
    [SerializeField] InputAction menuToggleAction;
    GameObject menu;
    PlayerController playerController;

    bool pauseToggle;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        menu = transform.GetChild(0).gameObject;
        menu.SetActive(false);
    }

    private void OnEnable()
    {
        menuToggleAction.performed += MenuToggle;
        menuToggleAction.Enable();
    }
    private void OnDisable()
    {
        menuToggleAction.Disable();
    }

    private void MenuToggle(InputAction.CallbackContext obj)
    {
        menu.SetActive(!menu.activeSelf);

        Pause();
    }

    private void Pause()
    {
        if (!pauseToggle)
        {
            pauseToggle = true;

            playerController.DisableControls();

            Time.timeScale = 0f;
        }
        
        else
        {
            pauseToggle = false;

            playerController.EnableControls();

            Time.timeScale = 1f;
        }
    }
}
