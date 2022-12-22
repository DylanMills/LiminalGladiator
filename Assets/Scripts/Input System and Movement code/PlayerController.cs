using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float turnSmoothTime = 0.2f;
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
    bool jumpPressed;
    bool midJump = false;
   
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
  [SerializeField]  public LayerMask groundLayer;
    private CapsuleCollider col;
    private Rigidbody rb;
    [SerializeField] const float jumpTimer=50f;
    float _jumpTimer;
    // Attacking
    [SerializeField] InputAction _attackInput;
    [SerializeField] InputAction _heavyAttackInput;

    Attack attackScript;

    // Input Vectors
    Vector2 moveInput;
    Vector3 moveVector;
    Vector3 velocity;



    Vector3 gravity = new Vector3(0.0f,-9.81f,0.0f);


    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer,
        QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        attackScript = GetComponent<Attack>();
        col = GetComponent<CapsuleCollider>();
        _jumpTimer = jumpTimer;

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
        _jumpInput.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        _jumpInput.canceled += ctx => jumpPressed = ctx.ReadValueAsButton();


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
        if (!character.isGrounded) character.Move(gravity * 3f * Time.deltaTime); //APPLY GRAVITY EVERY FRAME
    }

    void HandleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        moveVector = new Vector3(moveInput.x, 0, moveInput.y);

        float targetAngle = Mathf.Atan2(moveVector.normalized.x, moveVector.normalized.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        if (jumpPressed &&_jumpTimer>0)
        {
            character.Move(Vector3.up * jumpVelocity * Time.deltaTime * _jumpTimer);
                       _jumpTimer--;
        }
        if (IsGrounded())
        {                        _jumpTimer = jumpTimer;
        }
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

        character.Move((moveDir * (moveVector.magnitude * speed * (attackScript.isAttacking ? .25f : 1f)) + velocity) * Time.deltaTime);
    }

    void PerformAttack(InputAction.CallbackContext ctx)
    {
        attackScript.OnAttack(animator, attackHash);
    }
    void PerformHeavyAttack(InputAction.CallbackContext ctx)
    {
        attackScript.OnHeavyAttack(animator, attackHeavyHash);
    }

    void PerformJump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
    }
    // Input system Enable/Disable
    private void OnEnable()
    {
        EnableControls();
    }
    private void OnDisable()
    {
        DisableControls();
    }

    public void EnableControls()
    {
        _movementInput.Enable();
        _runInput.Enable();
        _jumpInput.Enable();
        _attackInput.Enable();
        _heavyAttackInput.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableControls()
    {
        _movementInput.Disable();
        _runInput.Disable();
        _jumpInput.Disable();
        _attackInput.Disable();
        _heavyAttackInput.Enable();

        Cursor.lockState = CursorLockMode.None;
    }
}
