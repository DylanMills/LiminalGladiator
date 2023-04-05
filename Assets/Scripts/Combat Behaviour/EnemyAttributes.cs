using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody body;
    VanishObj vanishScript;

    Transform playerTrans;
    PlayerAttributes playerAttributes;
    // TODO use animation hashes

    [SerializeField]
    Collider hitbox;

    float attackTimer;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackSpeedMargin;

    public int health = 100;

    [SerializeField] GameObject healthPickupPrefab;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        if(animator == null)
        {
            animator = transform.GetComponent<Animator>();
        }
        body = GetComponent<Rigidbody>();
        vanishScript = GetComponent<VanishObj>();

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        playerAttributes = playerTrans.GetComponent<PlayerAttributes>();
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
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void Damage(int damage, float knockback)
    {
        if (Vector3.Dot(DirToPlayer(), transform.forward) <= 0)
            damage *= 2;

        health -= damage;

        animator.SetTrigger("Stunned");
        body.AddForce((transform.position - playerTrans.position).normalized * knockback, ForceMode.Impulse);
        playerAttributes.BuildMeter(damage * 2);

        PlayerAudioController.PlayClip("hit" + UnityEngine.Random.Range(0, 2), transform.position);
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        Destroy(gameObject, 2f);

        if (UnityEngine.Random.Range(0, 3) == 0)
            Instantiate(healthPickupPrefab, transform.position + Vector3.up, Quaternion.identity);

        vanishScript.StartEffect(1.8f);

        PlayerAttributes.IncreaseKillCount();

        enabled = false;
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
