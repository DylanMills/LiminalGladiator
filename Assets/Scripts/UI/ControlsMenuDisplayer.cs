using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenuDisplayer : MonoBehaviour
{
    [SerializeField] InputAction menuToggleAction;
    GameObject menu;

    private void Start()
    {
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
    }
}
