using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour, IDamageable
{
    public void Damage(int damage)//When this interface is used by the other classes, this enemy will die.
    {
        Debug.Log($"I've been struck! -{damage}");
        Destroy(gameObject);
    }
}
