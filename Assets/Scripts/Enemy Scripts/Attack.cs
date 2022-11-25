using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float DamageAfterTime;//How long it takes for an attack to occur.

    [SerializeField]
    private float HeavyDamageAfterTime;//How long it takes for a heavy attack to occur.

    [SerializeField]
    private int damage;//Damage amount.

    [SerializeField]
    private AttackZone attackZone;//This represents our collider attack zone.

    private bool isAttacking;//Is the character attacking.
     
    public void OnAttack(/*InputValue val*/)//When attacking, the coroutine starts with the heavy boolean being false.
    {
        if (isAttacking) return;//This boolean let's us know that attacking process of the coroutine has begun.
        StartCoroutine(Strike(false));
        Debug.Log("Attack.");
    }
    public void OnHeavyAttack(/*InputValue val*/)//When heavily attacking, the coroutine starts with the heavy boolean being true.
    {
        if (isAttacking) return;
        StartCoroutine(Strike(true));
        Debug.Log("Heavy Attack.");
    }

    private IEnumerator Strike(bool heavy)
    {
        isAttacking = true;//The attacking process has started.
        yield return new WaitForSeconds(heavy ? HeavyDamageAfterTime : DamageAfterTime);
        foreach (var attackZoneDamageable in attackZone.Damageables)
        {
            attackZoneDamageable.Damage(damage * (heavy ? 3 : 1));
        }
        yield return new WaitForSeconds(heavy ? HeavyDamageAfterTime : DamageAfterTime);
        isAttacking = false;//The attacking process has ended.
    }
}
