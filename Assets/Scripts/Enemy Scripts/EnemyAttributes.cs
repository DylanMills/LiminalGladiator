using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody body;

    Transform playerTrans;

    // TODO use animation hashes

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
        transform.LookAt(new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z), Vector3.up);

        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0)
            Attack();
    }

    public void Damage(int damage, float knockback)
    {
        Debug.Log($"I've been struck! -{damage}");
        health -= damage;

        animator.SetTrigger("Stagger");
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

        attackTimer = attackSpeed + Random.Range(-attackSpeedMargin, attackSpeedMargin);
    }
}
