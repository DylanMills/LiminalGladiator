using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishScript : MonoBehaviour
{
    public Animator animator;
    public bool disintegrate = false;
    public GameObject particles;
    ParticleSystem.EmissionModule em;
    // Start is called before the first frame update
    void Start()
    {
        particles.GetComponent<ParticleSystem>().Stop();
        particles.GetComponent<ParticleSystem>().enableEmission = false;
        em = particles.GetComponent<ParticleSystem>().emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disintegrate == true)
        {
            animator.SetTrigger("Disintegrate");
            particles.GetComponent<ParticleSystem>().Play();
            em.enabled = true;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
