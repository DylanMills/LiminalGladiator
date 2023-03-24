using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    CharacterController Controller;
    ScreenShake screenShakeScript;

    public int health = 100;
    private bool invuln = false;
    public int meter = 0;
    public bool fullMeter = false;

    public GameObject fullMeterUI;
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        screenShakeScript = GetComponent<ScreenShake>();
        fullMeterUI.SetActive(false);
    }
    public void BuildMeter(int amount)
    {
        meter += amount;
        if (meter > 99)
        {
            screenShakeScript.ShakeScreen(.1f, .75f);

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
        screenShakeScript.ShakeScreen(2.5f, 1.25f);

        meter = 0;
        fullMeter = false;

        fullMeterUI.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (invuln) return;
        health -= damage;

        Controller.Move(-transform.forward);

        screenShakeScript.ShakeScreen(.25f, 2f);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            health = Mathf.Min(health + 20, 100);

            Destroy(other.gameObject);
        }
    }
}
