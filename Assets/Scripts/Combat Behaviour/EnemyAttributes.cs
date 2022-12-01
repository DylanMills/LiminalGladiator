using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody body;

    Transform playerTrans;

    // TODO use animation hashes

    [SerializeField]
    Collider hitbox;

    float attackTimer;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackSpeedMargin;

    public int health = 100;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, playerTrans.position) < 25)
        {
            Quaternion rot = Quaternion.LookRotation(DirToPlayer(), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 2);

            attackTimer -= Time.fixedDeltaTime;

            if (attackTimer <= 0)
                Attack();
        }
    }

    public void Damage(int damage, float knockback)
    {
        if (Vector3.Dot(DirToPlayer(), transform.forward) <= 0)
            damage *= 2;

            Debug.Log($"I've been struck! -{damage}");
        health -= damage;

        animator.SetTrigger("Stunned");
        body.AddForce((transform.position - playerTrans.position).normalized * knockback, ForceMode.Impulse);

        if (health <= 0)
            Die();
    }

    void Die()
    {
        // TODO play death animation, queue death events

        Destroy(gameObject);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        StartCoroutine(Strike());

        attackTimer = attackSpeed + UnityEngine.Random.Range(-attackSpeedMargin, attackSpeedMargin);
    }

    IEnumerator Strike()
    {
        hitbox.enabled = true;

        yield return new WaitForSeconds(.5f);

        hitbox.enabled = false;
    }

    Vector3 DirToPlayer()
    {
        return (new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z) - transform.position).normalized;
    }
}
