using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginAttack(int headNumber)
    {
        animator.SetInteger("AttackChoice", headNumber);
    }
    public void EndAttack()
    {
        animator.SetBool("AttackingThisCycle", false);
    }
    public void ChooseIdle()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        animator.SetInteger("Idle", Random.Range(1, 4));
    }
}
