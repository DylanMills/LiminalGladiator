using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboCounter : MonoBehaviour
{
    public int maxCombo;
    public int combo = 0;

   // Text comboCount;
    float lastHitTime = 0;
//    bool visible = true; 
    
    void Update()
    {
        if (IsComboBroken())    combo = 0;
    }

    public void ComboIncrement()
    {
        combo++;
        combo %= maxCombo;

        lastHitTime = Time.time;
     //   comboCount.text = combo.ToString();
   
    }

    bool IsComboBroken()
    {
        return Time.time - lastHitTime > .75f;
    }
}
