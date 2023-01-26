using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboCounter : MonoBehaviour
{
    public int maxAttackCombo;
    public int attackCombo;

    public int combo = 0;

    Text comboCount;
    float lastHitTime = 0;
    bool visible = true; 
    
    void Update()
    {
        if (IsComboBroken())
        {
            //comboCount.enabled = false;
            combo = 0;
        }
    }
    public void ComboIncrement()
    {
        comboCount.enabled = true;
        combo++;
        comboCount.text = combo.ToString();
        lastHitTime = Time.time;
    }

    public void AttackIncrement()
    {
        attackCombo++;
        if (attackCombo > maxAttackCombo) ResetAttackCounter();
        Invoke(nameof(ResetAttackCounter), 1f);
    }
    void ResetAttackCounter()
    {
        attackCombo = 0;
    }

    bool IsComboBroken()
    {
        return Time.time - lastHitTime >= 2;
    }
}
