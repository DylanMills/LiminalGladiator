using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    public List<IDamageable> Damageables { get; } = new(); //The interfaces are put in a list to apply to varying objects.
    [ExecuteAlways]
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable)) //When the an object with the interface enters the attack zone trigger, the object is..
        {
            if (!Damageables.Contains(damageable))
            {
                damageable.Damage(100);
            }

            Debug.Log("Shockwave Hit");

            Damageables.Add(damageable);                       //added to the list of interfaces.
        }
    }
}
