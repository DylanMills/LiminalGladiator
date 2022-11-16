using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerControls _inputActionsScript;

    public float speed = 2;
    public Rigidbody _rb;
    public Vector2 input;
    public GameObject characterBody;
    public Transform _camera;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }
    private void Start()
    {
        _inputActionsScript = new PlayerControls();
        _inputActionsScript.Player.Enable();
    }
    private void Update()
    {
    }
   
}
