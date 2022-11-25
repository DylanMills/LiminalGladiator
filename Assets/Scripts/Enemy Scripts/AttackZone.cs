using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public List<IDamageable> Damageables { get; } = new();//The interfaces are put in a list to apply to varying objects.

    public void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();//When the an object with the interface enters the attack zone trigger, the object is 
        if (damageable != null)                            //added to the list of interfaces.
        {
            Damageables.Add(damageable);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();//When the an object leaves the trigger, it is temporarily removed from the list until it
        if (damageable != null && Damageables.Contains(damageable))//enters the list again.
        {
            Damageables.Remove(damageable);
        }
    }
}
