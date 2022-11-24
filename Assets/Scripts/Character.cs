using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hitPoints = 100;
    public virtual void GetHit(int damage)
    {
        hitPoints -= damage;
    }
}
