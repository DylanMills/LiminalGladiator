using System;
using System.Collections;
using UnityEngine;

public class VanishObj : MonoBehaviour
{
    [SerializeField] Material vanishEffectMaterial;
    [SerializeField] Renderer[] objRenderers;
    //List<Material> objMaterialRefs = new();

    private void Awake()
    {
        
    }

    public void StartEffect(float length)
    {
        foreach (Renderer renderer in objRenderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = vanishEffectMaterial;
            }
            renderer.materials = materials;
        }

        StartCoroutine(VanishEffectFade(length));
    }

    IEnumerator VanishEffectFade(float length)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * (1f / length);

            vanishEffectMaterial.SetFloat("VanishAmount", t);

            yield return null;
        }

        vanishEffectMaterial.SetFloat("VanishAmount", 0);
    }
}
