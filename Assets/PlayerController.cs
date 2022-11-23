using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    public Controls input;
    CharacterController character;
    bool grounded = false;
    [SerializeField] float speed;
    [SerializeField] float runMultiplier;
    int runFloatHash;
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;
    Vector3 moveVector;
    Vector3 v;
    
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        input = new Controls();
        currentMovement = Vector2.zero;
        input.Player.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            if (currentMovement != Vector2.zero) { movementPressed = true; }
            else
            {
                movementPressed = false;
            }
        }
        ;
        input.Player.Move.canceled += ctx =>
        {
            movementPressed = false;
            currentMovement = Vector2.zero;
        };
        input.Player.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.Player.Run.canceled += ctx => runPressed = ctx.ReadValueAsButton();
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
        //     bool Attack = animator.GetBool(isWalkingHash);
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


     //   print(currentMovement);
       // print(movementPressed);
       // print(runPressed);
    }
    private void OnEnable()
    {
        input.Player.Enable();

    }
    private void OnDisable()
    {
        input.Player.Disable();
    }
}
