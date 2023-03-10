using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public List<IDamageable> Damageables { get; } = new(); //The interfaces are put in a list to apply to varying objects.
    int hitDamage;
    public GameObject blood;
    public GameObject shockwavePrefab;
    Collider hitbox;

    private void Awake()
    {
        if (hitbox == null)
            hitbox = GetComponent<Collider>();

        hitbox.isTrigger = true;
        DisableHitbox();
    }

    public void EnableHitbox(int damage)
    {
        hitDamage = damage;
        hitbox.enabled = true;
    }
    public void DisableHitbox()
    {
        hitbox.enabled = false;
        hitDamage = 0;
        Damageables.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable)) //When the an object with the interface enters the attack zone trigger, the object is..
        {
            if (!Damageables.Contains(damageable))
            {
                blood.GetComponent<ParticleSystem>().Emit(30);
                damageable.Damage(hitDamage);
            }

            Debug.Log("found damageable");

            Damageables.Add(damageable);                       //added to the list of interfaces.
        }
        if (other.gameObject.tag == "Ground")
        {
            SpawnShockwave(new Vector3(transform.position.x, transform.position.y+5, transform.position.z));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        blood.GetComponent<ParticleSystem>().Emit(0);
    }

    public void SpawnShockwave(Vector3 location)
    {
        Instantiate(shockwavePrefab, location, Quaternion.identity);
    }
    /*
    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))  //When the an object leaves the trigger, it is temporarily removed from the list until it
            if (Damageables.Contains(damageable))               //enters the list again.
            {
                Damageables.Remove(damageable);
            }
    }
    */
}
