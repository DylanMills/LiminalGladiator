using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTester : MonoBehaviour
{
    public GameObject shockwavePrefab;
    public bool shockwaveSpawn = false;
    void Update()
    {
        if (shockwaveSpawn)
        {
            SpawnShockwave(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z));
            shockwaveSpawn = false;
        }
    }
    void SpawnShockwave(Vector3 location)
    {
        Instantiate(shockwavePrefab, location, Quaternion.identity);
    }
}
