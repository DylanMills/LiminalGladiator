using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public ComboCounter comboCounter;
    private void Awake()
    {
        comboCounter = GameObject.Find("Canvas").GetComponent<ComboCounter>();
    }
    public override void GetHit(int damage)
    {
        base.GetHit(damage);
        comboCounter.ComboIncrement();
    }
}
