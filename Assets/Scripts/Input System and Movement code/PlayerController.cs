using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Animation Vars:
    CharacterController character;
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int runFloatHash;
    int attackHash;
    int attackHeavyHash;

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
    [SerializeField] InputAction _heavyAttackInput;

    Attack attackScript;

    // Input Vectors
    Vector2 moveInput;
    Vector3 moveVector;
    Vector3 velocity;



    Vector3 gravity = new Vector3(0.0f,-9.81f,0.0f);




    private void Awake()
    {
        character = GetComponent<CharacterController>();
        attackScript = GetComponent<Attack>();

        moveInput = Vector2.zero;

        _movementInput.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();

            if (moveInput != Vector2.zero)
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
            moveInput = Vector2.zero;
        };

        _runInput.performed += ctx => runPressed = ctx.ReadValueAsButton();
        _runInput.canceled += ctx => runPressed = ctx.ReadValueAsButton();

        _attackInput.performed += PerformAttack;
        _heavyAttackInput.performed += PerformHeavyAttack;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        runFloatHash = Animator.StringToHash("runMultiplier");
        attackHash = Animator.StringToHash("Attack");
        attackHeavyHash = Animator.StringToHash("AttackHeavy");
    }

    private void Update()
    {
        HandleMovement();
        if (!character.isGrounded) character.Move(gravity * Time.deltaTime); //APPLY GRAVITY EVERY FRAME
    }

    void HandleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        moveVector = (new Vector3(moveInput.x, 0, moveInput.y));

        float targetAngle = Mathf.Atan2(moveVector.normalized.x, moveVector.normalized.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


        if (movementPressed)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if (!isWalking) animator.SetBool(isWalkingHash, true);//start moving

            if (runPressed)
            {
                animator.SetFloat(runFloatHash, 1.5f);
                moveVector *= runMultiplier;

                if (!isRunning) animator.SetBool(isRunningHash, true);//start running

            }
            else animator.SetFloat(runFloatHash, 1.0f);
        }
        else
        {
            if (isWalking) animator.SetBool(isWalkingHash, false);//stop moving animation
            if (isRunning) animator.SetBool(isRunningHash, false);//stop running bool
        }

        character.Move((moveDir * moveVector.magnitude * speed + velocity) * Time.deltaTime);
    }

    void PerformAttack(InputAction.CallbackContext ctx)
    {
        attackScript.OnAttack(animator, attackHash);
    }
    void PerformHeavyAttack(InputAction.CallbackContext ctx)
    {
        attackScript.OnHeavyAttack(animator, attackHeavyHash);
    }

    // Input system Enable/Disable
    private void OnEnable()
    {
        _movementInput.Enable();
        _runInput.Enable();
        _jumpInput.Enable();
        _attackInput.Enable();
        _heavyAttackInput.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        _movementInput.Disable();
        _runInput.Disable();
        _jumpInput.Disable();
        _attackInput.Disable();
        _heavyAttackInput.Enable();

        Cursor.lockState = CursorLockMode.None;
    }
}
