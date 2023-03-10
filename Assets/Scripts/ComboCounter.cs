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
        if (IsComboBroken())    ResetCombo();

    }
    public void ComboIncrement()
    {
      
        if (combo >= maxCombo) ResetCombo();
        combo++;
        lastHitTime = Time.time;
     //   comboCount.text = combo.ToString();
   
    }


    void ResetCombo()
    {
        combo = 0;
    }

    bool IsComboBroken()
    {
        return Time.time - lastHitTime >= 1;
    }
}
