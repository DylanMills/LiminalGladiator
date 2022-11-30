using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    CharacterController controller;

    public int health = 100;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        controller.Move(-transform.forward);

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
