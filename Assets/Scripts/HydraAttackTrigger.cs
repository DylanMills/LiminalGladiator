using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraAttackTrigger : MonoBehaviour
{
    public int headNum = 0;
    public HydraScript hydra;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hydra.BeginAttack(headNum);
        }
    }
}
