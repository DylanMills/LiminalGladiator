using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxScript : MonoBehaviour
{
    public Enemy parentCharacter;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HitboxScript>() != null)
        {
            parentCharacter.GetHit(collision.gameObject.GetComponent<HitboxScript>().damage);
        }
    }
}
