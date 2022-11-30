using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int baseDamage;
    [SerializeField]
    private float attackStartup;//How long it takes for an attack to occur.
    [SerializeField]
    private float attackLength;//How long until the attack should stop checking for damageables
    [SerializeField]
    private float heavyAttackStartup;
    [SerializeField]
    private float heavyAttackLength;

    [SerializeField]
    private AttackZone attackZone;//This represents our collider attack zone.

    private bool isAttacking;//Is the character attacking?
     
    public void OnAttack(Animator animator, int animHash)
    {
        if (isAttacking) return;//This boolean lets us know that attacking process of the coroutine has begun.

        StartCoroutine(Strike(baseDamage, attackStartup, attackLength));
        animator.SetTrigger(animHash);
    }
    public void OnHeavyAttack(Animator animator, int animHash)
    {
        if (isAttacking) return;

        StartCoroutine(Strike(baseDamage * 3, heavyAttackStartup, heavyAttackLength));
        animator.SetTrigger(animHash);
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
}
