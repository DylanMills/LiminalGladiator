using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    // Animation Vars:
    CharacterController character;
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int runFloatHash;

    // Input Vars:
    public Controls input;

    // Walking
    [SerializeField] InputAction _movementInput;
    bool movementPressed;
    [SerializeField] float speed;

    // Running
    [SerializeField] InputAction _runInput;
    bool runPressed;
    [SerializeField] float runMultiplier;
    
    // Jumping
    [SerializeField] InputAction _jumpInput;
    [SerializeField] float jumpMultiplier;
    bool _isGrounded = false;
    
    // Attacking
    [SerializeField] InputAction _attackInput;

    // Input Vectors
    Vector2 currentMovement;
    Vector3 moveVector;
    Vector3 v;
    
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        currentMovement = Vector2.zero;

        _movementInput.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();

            if (currentMovement != Vector2.zero)
            {
                movementPressed = true;
            }
            else
            {
                movementPressed = false;
            }
        };

        _movementInput.canceled += ctx =>
        {
            movementPressed = false;
            currentMovement = Vector2.zero;
        };

        _runInput.performed += ctx => runPressed = ctx.ReadValueAsButton();
        _runInput.canceled += ctx => runPressed = ctx.ReadValueAsButton();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        runFloatHash = Animator.StringToHash("runMultiplier");

    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        moveVector = (new Vector3(currentMovement.x, 0, currentMovement.y));

        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        // bool Attack = animator.GetBool(isWalkingHash);

        if (movementPressed && !isWalking)//start moving
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (!movementPressed && isWalking)//stop
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((movementPressed && runPressed) && !isRunning)//start running
        {
            moveVector *= runMultiplier;
            animator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed || !runPressed) && isRunning)//stop running
        {
            animator.SetBool(isRunningHash, false);
        }

        if (currentMovement != Vector2.zero)
        {
            // Run modifier
            if (runPressed)
            {
                animator.SetFloat(runFloatHash, runMultiplier);
                character.Move(moveVector * speed *runMultiplier* Time.deltaTime);
            }
            else
            {
                animator.SetFloat(runFloatHash, 1.0f);
                character.Move(moveVector * speed * Time.deltaTime);
            }

            gameObject.transform.forward = moveVector;
        }

        character.Move(v * Time.deltaTime);

        // print(currentMovement);
        // print(movementPressed);
        // print(runPressed);
    }

    // Input system Enable/Disable
    private void OnEnable()
    {
        _movementInput.Enable();
        _runInput.Enable();
        _jumpInput.Enable();
        _attackInput.Enable();

    }
    private void OnDisable()
    {
        _movementInput.Disable();
        _runInput.Disable();
        _jumpInput.Disable();
        _attackInput.Disable();
    }
}
