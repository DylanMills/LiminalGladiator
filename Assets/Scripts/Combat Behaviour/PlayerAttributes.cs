using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    CharacterController Controller;

    public int health = 100;
    private bool invuln = false;
    public int meter = 0;
    public bool fullMeter = false;

    public GameObject fullMeterUI;
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        fullMeterUI.SetActive(false);
    }
    public void BuildMeter(int amount)
    {
        meter += amount;
        if (meter > 99)
        {
            meter = 100;
            fullMeter = true;
            fullMeterUI.SetActive(true);
        }
        else
        {
            fullMeter = false;
            fullMeterUI.SetActive(false);
        }
    }
    public void SpendMeter()
    {
        if (fullMeter)
        {
            meter = 0;
            fullMeter = false;
        }


    }
    public void TakeDamage(int damage)
    {
        if (invuln) return;
        health -= damage;

        Controller.Move(-transform.forward);

        if (health <= 0)
            Die();


    }
    public void GoInvuln()
    {
        invuln = true;
    }
    public void StopInvuln()
    {
        invuln = false;
    }

    void Die()
    {
        Debug.Log("Died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
