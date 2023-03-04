using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public float dodgeDistance = 20f; // the distance to dodge
    public float dodgeTime = 0.75f; // the duration of the dodge
    public KeyCode dodgeButton = KeyCode.R; // the button to trigger the dodge
    public AnimationCurve dodgeCurve; // the curve used to lerp the dodge distance

    public List<SkinnedMeshRenderer> playerRenderer; // Mesh renderers to disable while dodging
    public List<GameObject> playerObjects; // objects to be disabled while dodging


    public ParticleSystem particles;
    private bool isDodging = false; // flag to check if player is currently dodging
    private CharacterController controller; // reference to the character controller component
    private PlayerController playerController;
    private PlayerAttributes playerAttributes;
    // start method to get reference to character controller component
    void Start()
    {

        playerAttributes = GetComponent<PlayerAttributes>();
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    // update method to check for input and trigger dodge
    void Update()
    {
        if (Input.GetKeyDown(dodgeButton) && !isDodging)
        {
            StartCoroutine(DoDodge());
        }
    }

    // coroutine to perform the dodge
    IEnumerator DoDodge()
    {
        ActivateDodge();
        float timer = 0f;
        Vector3 dodgeDirection = transform.forward * dodgeDistance;
        while (timer < dodgeTime)
        {
            float dodgeProgress = dodgeCurve.Evaluate(timer / dodgeTime);
            Vector3 moveVector = Vector3.Lerp(Vector3.zero, dodgeDirection, dodgeProgress);
            controller.Move(moveVector * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        DeactivateDodge();

    }

    void ActivateDodge()
    {
        isDodging = true;
        foreach (var mesh in playerRenderer)
        {
            mesh.enabled = false;
        }
        foreach (var obj in playerObjects)
        {
            obj.SetActive(false);
        }
        playerController.DisableControls();
        particles.Play();
        playerAttributes.GoInvuln();


    }
    void DeactivateDodge()
    {
        playerController.EnableControls();
        foreach (var mesh in playerRenderer)
        {
            mesh.enabled = true;
        }
        foreach (var obj in playerObjects)
        {
            obj.SetActive(true);
        }
        playerAttributes.StopInvuln();
        isDodging = false;
    }
}