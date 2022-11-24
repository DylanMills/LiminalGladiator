using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboCounter : MonoBehaviour
{
    public int combo = 0;
    public Text comboCount;
    public float lastHitTime = 0;
    public bool visible = true; 
    private IEnumerator timer;
    public void Start()
    {
    }
    public void Update()
    {
        if(Time.time - lastHitTime >= 2)
        {
            comboCount.enabled = false;
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
}
