using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;
using UnityEngine.InputSystem.XR;

public class Burst : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem particles;
    private bool isBursting = false; // flag to check if player is currently bursting
    private CharacterController controller; // reference to the character controller component
    private PlayerController playerController;
    private PlayerAttributes playerAttributes;

    [SerializeField]
    private int baseDamage;
    [SerializeField]
    private float attackStartup;//How long it takes for an attack to occur.
    [SerializeField]
    private float attackLength;//How long until the attack should stop checking for damageables


    [SerializeField]
    private AttackZone attackZone;//This represents our collider attack zone.

    public bool isAttacking;//Is the character attacking?


    // start method to get reference to character controller component
    void Start()
    {
        // particles = GetComponent<ParticleSystem>();
        playerAttributes = GetComponent<PlayerAttributes>();
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }
    public void OnAttack()
    {
        if (isAttacking) return;//This boolean lets us know that attacking process of the coroutine has begun.



        StartCoroutine(Strike(baseDamage, attackStartup, attackLength));
    }


    private IEnumerator Strike(int damage, float startup, float hitLength)
    {
        isAttacking = true;//The attacking process has started.
        yield return new WaitForSeconds(startup);

        attackZone.EnableHitbox(damage);

        yield return new WaitForSeconds(hitLength);

        attackZone.DisableHitbox();

        isAttacking = false;//The attacking process has ended.
    }

    // called from PlayerController upon a burst input action
    public void InputBurst(Animator animator, int hash)
    {
        if (playerAttributes.fullMeter)
        {
            animator.SetTrigger(hash);
            StartCoroutine(DoBurst());
        }
    }

    // coroutine to perform the burst
    IEnumerator DoBurst()
    {
        yield return new WaitForSeconds(.2f); // animation startup

        ActivateBurst();
        float timer = 0f;

        while (timer < attackLength)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        DeactivateBurst();

    }

    void ActivateBurst()
    {
        isBursting = true;
        playerController.DisableControls();
        particles.Play();
        playerAttributes.GoInvuln();
        playerAttributes.SpendMeter();
        attackZone.EnableHitbox(baseDamage);


    }
    void DeactivateBurst()
    {
        playerController.EnableControls();
        playerAttributes.StopInvuln();
        attackZone.DisableHitbox();

        isBursting = false;
    }
}
